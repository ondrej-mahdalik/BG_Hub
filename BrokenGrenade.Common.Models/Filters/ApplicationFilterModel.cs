using BrokenGrenade.Common.Enums;
namespace BrokenGrenade.Common.Models.Filters;

public class ApplicationFilterModel
{
    public string? Nickname { get; set; }
    public string? Discord { get; set; }
    public string? Email { get; set; }
    public string? Steam { get; set; }
    public int? Age { get; set; }
    public ApplicationStatus? Status { get; set; }
    public Guid? UserId { get; set; }
    public Guid? EditedById { get; set; }
    public DateTime? CreatedAt { get; set; }
}
