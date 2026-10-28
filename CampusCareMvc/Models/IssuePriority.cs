using System.ComponentModel.DataAnnotations;

namespace CampusCareMvc.Models;

public enum IssuePriority
{
    [Display(Name = "Low")]
    Low = 1,
    [Display(Name = "Normal")]
    Normal = 2,
    [Display(Name = "High")]
    High = 3,
    [Display(Name = "Urgent")]
    Urgent = 4
}
