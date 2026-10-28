using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace CampusCareMvc.Models;

[Index(nameof(TicketNumber), IsUnique = true)]
public class Complaint
{
    public int Id { get; set; }

    [Display(Name = "Ticket number")]
    [StringLength(24)]
    public string TicketNumber { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please enter a short title.")]
    [StringLength(100, MinimumLength = 5, ErrorMessage = "The title must be between 5 and 100 characters.")]
    [Display(Name = "Issue title")]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please describe the issue.")]
    [StringLength(1200, MinimumLength = 15, ErrorMessage = "The description must be between 15 and 1,200 characters.")]
    [Display(Name = "Description")]
    public string Description { get; set; } = string.Empty;

    [Required(ErrorMessage = "Choose a category.")]
    [EnumDataType(typeof(IssueCategory), ErrorMessage = "Choose a valid category.")]
    [Display(Name = "Category")]
    public IssueCategory Category { get; set; }

    [Required(ErrorMessage = "Choose a priority.")]
    [EnumDataType(typeof(IssuePriority), ErrorMessage = "Choose a valid priority.")]
    [Display(Name = "Priority")]
    public IssuePriority Priority { get; set; }

    [EnumDataType(typeof(IssueStatus), ErrorMessage = "Choose a valid status.")]
    [Display(Name = "Status")]
    public IssueStatus Status { get; set; } = IssueStatus.Submitted;

    [Required(ErrorMessage = "Enter the campus location.")]
    [StringLength(120, MinimumLength = 2, ErrorMessage = "The location must be between 2 and 120 characters.")]
    [Display(Name = "Campus location")]
    public string Location { get; set; } = string.Empty;

    [Required(ErrorMessage = "Enter your name.")]
    [StringLength(80, MinimumLength = 2, ErrorMessage = "The name must be between 2 and 80 characters.")]
    [Display(Name = "Reported by")]
    public string ReporterName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Enter your college email address.")]
    [EmailAddress(ErrorMessage = "Enter a valid email address.")]
    [StringLength(120)]
    [Display(Name = "Email address")]
    public string ReporterEmail { get; set; } = string.Empty;

    [Display(Name = "Date reported")]
    public DateTime DateReported { get; set; } = DateTime.Now;

    [Display(Name = "Last updated")]
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}
