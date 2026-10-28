using System.Text.Json;
using CampusCareMvc.Data;
using CampusCareMvc.Models;
using CampusCareMvc.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CampusCareMvc.Controllers;

public class ComplaintsController(ApplicationDbContext db) : Controller
{
    private const string RecentlyViewedSessionKey = "RecentlyViewedComplaintIds";
    private const int RecentlyViewedLimit = 5;

    public async Task<IActionResult> Index(string? searchTerm, IssueStatus? status, IssueCategory? category)
    {
        var query = db.Complaints.AsNoTracking().AsQueryable();

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            var search = searchTerm.Trim();
            query = query.Where(c => c.Title.Contains(search)
                || c.TicketNumber.Contains(search)
                || c.Location.Contains(search)
                || c.ReporterName.Contains(search));
        }

        if (status.HasValue)
        {
            query = query.Where(c => c.Status == status.Value);
        }

        if (category.HasValue)
        {
            query = query.Where(c => c.Category == category.Value);
        }

        var complaintList = await query
            .OrderByDescending(c => c.DateReported)
            .ToListAsync();

        var recentlyViewedIds = GetRecentlyViewedIds();
        var recentlyViewed = recentlyViewedIds.Count == 0
            ? new List<Complaint>()
            : await db.Complaints.AsNoTracking()
                .Where(c => recentlyViewedIds.Contains(c.Id))
                .ToListAsync();
        recentlyViewed = recentlyViewed
            .OrderBy(c => recentlyViewedIds.IndexOf(c.Id))
            .ToList();

        var model = new ComplaintIndexViewModel
        {
            SearchTerm = searchTerm,
            SelectedStatus = status,
            SelectedCategory = category,
            Complaints = complaintList,
            RecentlyViewed = recentlyViewed
        };

        return View(model);
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var complaint = await db.Complaints.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
        if (complaint is null)
        {
            return NotFound();
        }

        RecordRecentlyViewed(complaint.Id);
        return View(complaint);
    }

    [HttpGet]
    public IActionResult Create() => View(new Complaint { Status = IssueStatus.Submitted });

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Complaint complaint)
    {
        if (!ModelState.IsValid)
        {
            return View(complaint);
        }

        complaint.TicketNumber = $"CC-{DateTime.Now:yyyy}-{Guid.NewGuid().ToString("N")[..8].ToUpperInvariant()}";
        complaint.Status = IssueStatus.Submitted;
        complaint.DateReported = DateTime.Now;
        complaint.UpdatedAt = complaint.DateReported;

        db.Complaints.Add(complaint);
        await db.SaveChangesAsync();

        TempData["SuccessMessage"] = $"Complaint {complaint.TicketNumber} was submitted successfully.";
        return RedirectToAction(nameof(Details), new { id = complaint.Id });
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var complaint = await db.Complaints.FindAsync(id.Value);
        return complaint is null ? NotFound() : View(complaint);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,Category,Priority,Status,Location,ReporterName,ReporterEmail")] Complaint input)
    {
        if (id != input.Id)
        {
            return BadRequest();
        }

        if (!ModelState.IsValid)
        {
            var saved = await db.Complaints.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
            if (saved is null)
            {
                return NotFound();
            }

            input.TicketNumber = saved.TicketNumber;
            input.DateReported = saved.DateReported;
            input.UpdatedAt = saved.UpdatedAt;
            return View(input);
        }

        var complaint = await db.Complaints.FirstOrDefaultAsync(c => c.Id == id);
        if (complaint is null)
        {
            return NotFound();
        }

        complaint.Title = input.Title;
        complaint.Description = input.Description;
        complaint.Category = input.Category;
        complaint.Priority = input.Priority;
        complaint.Status = input.Status;
        complaint.Location = input.Location;
        complaint.ReporterName = input.ReporterName;
        complaint.ReporterEmail = input.ReporterEmail;
        complaint.UpdatedAt = DateTime.Now;

        await db.SaveChangesAsync();
        TempData["SuccessMessage"] = $"Complaint {complaint.TicketNumber} was updated.";
        return RedirectToAction(nameof(Details), new { id = complaint.Id });
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var complaint = await db.Complaints.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
        return complaint is null ? NotFound() : View(complaint);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var complaint = await db.Complaints.FindAsync(id);
        if (complaint is null)
        {
            return NotFound();
        }

        var ticketNumber = complaint.TicketNumber;
        db.Complaints.Remove(complaint);
        await db.SaveChangesAsync();

        var recentIds = GetRecentlyViewedIds();
        recentIds.Remove(id);
        HttpContext.Session.SetString(RecentlyViewedSessionKey, JsonSerializer.Serialize(recentIds));

        TempData["SuccessMessage"] = $"Complaint {ticketNumber} was deleted.";
        return RedirectToAction(nameof(Index));
    }

    private List<int> GetRecentlyViewedIds()
    {
        var json = HttpContext.Session.GetString(RecentlyViewedSessionKey);
        if (string.IsNullOrWhiteSpace(json))
        {
            return [];
        }

        try
        {
            return JsonSerializer.Deserialize<List<int>>(json) ?? [];
        }
        catch (JsonException)
        {
            return [];
        }
    }

    private void RecordRecentlyViewed(int id)
    {
        var ids = GetRecentlyViewedIds();
        ids.Remove(id);
        ids.Insert(0, id);
        if (ids.Count > RecentlyViewedLimit)
        {
            ids = ids.Take(RecentlyViewedLimit).ToList();
        }

        HttpContext.Session.SetString(RecentlyViewedSessionKey, JsonSerializer.Serialize(ids));
    }
}
