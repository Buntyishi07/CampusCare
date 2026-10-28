using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace CampusCareMvc.Models;

public static class EnumDisplayExtensions
{
    public static string ToDisplayName(this Enum value)
    {
        var member = value.GetType().GetMember(value.ToString()).FirstOrDefault();
        return member?.GetCustomAttribute<DisplayAttribute>()?.GetName() ?? value.ToString();
    }
}
