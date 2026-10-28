using CampusCareMvc.Models;
using Microsoft.EntityFrameworkCore;

namespace CampusCareMvc.Data;

public static class DbInitializer
{
    public static async Task SeedAsync(ApplicationDbContext db)
    {
        if (await db.Complaints.AnyAsync())
        {
            return;
        }

        var now = DateTime.Now;
        var demoComplaints = new List<Complaint>
        {
            new()
            {
                TicketNumber = "CC-2026-0001",
                Title = "Projector is not turning on",
                Description = "The projector in the seminar room does not power on. The power indicator stays off even when the cable is connected.",
                Category = IssueCategory.ClassroomEquipment,
                Priority = IssuePriority.High,
                Status = IssueStatus.InProgress,
                Location = "Block A, Seminar Room 204",
                ReporterName = "Aarav Patel",
                ReporterEmail = "aarav@example.edu",
                DateReported = now.AddDays(-2),
                UpdatedAt = now.AddHours(-10)
            },
            new()
            {
                TicketNumber = "CC-2026-0002",
                Title = "Water tap leaking near lab",
                Description = "The washbasin tap near the computer lab keeps dripping and leaves water on the floor.",
                Category = IssueCategory.Plumbing,
                Priority = IssuePriority.Normal,
                Status = IssueStatus.Submitted,
                Location = "Block B, First-floor washroom",
                ReporterName = "Meera Shah",
                ReporterEmail = "meera@example.edu",
                DateReported = now.AddDays(-1),
                UpdatedAt = now.AddDays(-1)
            },
            new()
            {
                TicketNumber = "CC-2026-0003",
                Title = "Wi-Fi signal is weak",
                Description = "The wireless connection keeps disconnecting during lectures in the east-side classrooms.",
                Category = IssueCategory.Internet,
                Priority = IssuePriority.High,
                Status = IssueStatus.Resolved,
                Location = "Block C, Rooms 301–305",
                ReporterName = "Dev Joshi",
                ReporterEmail = "dev@example.edu",
                DateReported = now.AddDays(-5),
                UpdatedAt = now.AddDays(-2)
            },
            new()
            {
                TicketNumber = "CC-2026-0004",
                Title = "Broken chair in classroom",
                Description = "One chair has a loose backrest and should be repaired or replaced before the next class.",
                Category = IssueCategory.Furniture,
                Priority = IssuePriority.Low,
                Status = IssueStatus.Submitted,
                Location = "Block A, Room 112",
                ReporterName = "Nisha Desai",
                ReporterEmail = "nisha@example.edu",
                DateReported = now.AddHours(-6),
                UpdatedAt = now.AddHours(-6)
            }
        };

        await db.Complaints.AddRangeAsync(demoComplaints);
        await db.SaveChangesAsync();
    }
}
