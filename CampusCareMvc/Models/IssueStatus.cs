using System.ComponentModel.DataAnnotations;

namespace CampusCareMvc.Models;

public enum IssueStatus
{
    [Display(Name = "Submitted")]
    Submitted = 1,
    [Display(Name = "In progress")]
    InProgress = 2,
    [Display(Name = "Resolved")]
    Resolved = 3,
    [Display(Name = "Closed")]
    Closed = 4
}
