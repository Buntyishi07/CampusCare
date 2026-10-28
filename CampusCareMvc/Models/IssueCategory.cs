using System.ComponentModel.DataAnnotations;

namespace CampusCareMvc.Models;

public enum IssueCategory
{
    [Display(Name = "Electrical")]
    Electrical = 1,
    [Display(Name = "Plumbing")]
    Plumbing = 2,
    [Display(Name = "Cleaning")]
    Cleaning = 3,
    [Display(Name = "Furniture")]
    Furniture = 4,
    [Display(Name = "Internet / Wi-Fi")]
    Internet = 5,
    [Display(Name = "Classroom Equipment")]
    ClassroomEquipment = 6,
    [Display(Name = "Other")]
    Other = 7
}
