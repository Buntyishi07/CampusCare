using CampusCareMvc.Data;
using CampusCareMvc.Models;
using CampusCareMvc.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CampusCareMvc.Controllers;

public class HomeController(ApplicationDbContext db) : Controller
{
    public async Task<IActionResult> Index()
    {
        var model = new DashboardViewModel
        {
            TotalCount = await db.Complaints.CountAsync(),
            SubmittedCount = await db.Complaints.CountAsync(c => c.Status == IssueStatus.Submitted),
            InProgressCount = await db.Complaints.CountAsync(c => c.Status == IssueStatus.InProgress),
            ResolvedCount = await db.Complaints.CountAsync(c => c.Status == IssueStatus.Resolved || c.Status == IssueStatus.Closed),
            RecentComplaints = await db.Complaints.AsNoTracking()
                .OrderByDescending(c => c.DateReported)
                .Take(5)
                .ToListAsync()
        };

        return View(model);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error() => View();
}
