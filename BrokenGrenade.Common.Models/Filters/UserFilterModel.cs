namespace BrokenGrenade.Common.Models.Filters;

public class UserFilterModel
{
    public string? Nickname { get; set; }
    public string? Email { get; set; }
    public Guid? RoleId { get; set; }
}
