namespace BrokenGrenade.Common.Models.Filters;

public class PunishmentFilterModel
{
    public string? PunishmentName { get; set; }
    public bool IsActive { get; set; }
    public string? UserNickname { get; set; }
    public string? IssuedByNickname { get; set; }
}