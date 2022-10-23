using System.ComponentModel.DataAnnotations;

namespace BrokenGrenade.Models;

public class MissionCreateModel
{
    [Required]
    public string? Name { get; set; }
    [Required]
    [Url]
    public string? SlottingSheetUrl { get; set; }
    [Required]
    public DateTime? Date { get; set; }
    public bool IsLeaderBriefing { get; set; }
    public DateTime? LeaderBriefing { get; set; }
    [Required]
    public DateTime? ConectingToServer { get; set; }
    [Required]
    public DateTime? Start { get; set; }
    public string? ModpackUrl { get; set; }
    [Required]
    public Guid? CategoryId { get; set; }
    public Guid? ModpackTypeId { get; set; }
}