namespace BrokenGrenade.Common.Models.Filters;

public class RoleFilterModel
{
    public string? RoleName { get; set; }
    public bool IsActive { get; set; }
    public string? UserNickname { get; set; }
    public string? IssuedByNickname { get; set; }
}