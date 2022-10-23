using System.ComponentModel.DataAnnotations;
using BrokenGrenade.Data;

namespace BrokenGrenade.Models;

public class MissionEditModel
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

    public Mission ToMission(Guid? missionId = null)
    {
        if (!Date.HasValue || !Start.HasValue || !ConectingToServer.HasValue || Name is null || SlottingSheetUrl is null)
            throw new InvalidDataException();

        var start = Date.Value.Date + Start.Value.TimeOfDay;
        var connecting =Date.Value.Date + ConectingToServer.Value.TimeOfDay;        
        
        return new Mission(Name, SlottingSheetUrl, start, connecting, LeaderBriefing, ModpackUrl)
        {
            Id = missionId ?? Guid.NewGuid(),
            CategoryId = CategoryId,
            ModpackTypeId = ModpackTypeId
        };
    }

    public static MissionEditModel FromMission(Mission mission)
    {
        return new MissionEditModel
        {
            Name = mission.Name,
            SlottingSheetUrl = mission.SlottingSheetUrl,
            Date = mission.Start.Date,
            ConectingToServer = mission.ConnectingToServer,
            Start = mission.Start,
            LeaderBriefing = mission.LeaderBriefing,
            IsLeaderBriefing = mission.LeaderBriefing is not null,
            CategoryId = mission.CategoryId,
            ModpackTypeId = mission.ModpackTypeId,
            ModpackUrl = mission.ModpackUrl
        };
    }
}