namespace BrokenGrenade.Common.Models;

public class MissionFilterModel
{
    public string? Name { get; set; }
    public string? Creator { get; set; }
    public DateTime? Date { get; set; }
    public Guid? MissionType { get; set; }
    public Guid? ModpackType { get; set; }
}