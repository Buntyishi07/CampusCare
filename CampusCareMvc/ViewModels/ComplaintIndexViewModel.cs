using CampusCareMvc.Models;

namespace CampusCareMvc.ViewModels;

public class ComplaintIndexViewModel
{
    public string? SearchTerm { get; init; }
    public IssueStatus? SelectedStatus { get; init; }
    public IssueCategory? SelectedCategory { get; init; }
    public List<Complaint> Complaints { get; init; } = [];
    public List<Complaint> RecentlyViewed { get; init; } = [];
}
