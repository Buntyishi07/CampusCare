using CampusCareMvc.Models;

namespace CampusCareMvc.ViewModels;

public class DashboardViewModel
{
    public int TotalCount { get; init; }
    public int SubmittedCount { get; init; }
    public int InProgressCount { get; init; }
    public int ResolvedCount { get; init; }
    public List<Complaint> RecentComplaints { get; init; } = [];
}
