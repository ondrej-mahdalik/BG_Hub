using System.ComponentModel.DataAnnotations;
using BrokenGrenade.Data;

namespace BrokenGrenade.Models;

public class MissionEditModel
{
    [Required(ErrorMessage = "Název mise je vyžadován")]
    [MaxLength(100)]
    public string? Name { get; set; }
    
    [Required(ErrorMessage = "Odkaz na slotování je vyžadován")]
    [Url]
    public string? SlottingSheetUrl { get; set; }
    
    [Required(ErrorMessage = "Datum konání je vyžadováno")]
    public DateTime? Date { get; set; }
    
    public bool IsLeaderBriefing { get; set; }
    
    public DateTime? LeaderBriefing { get; set; }

    [Required(ErrorMessage = "Čas připojování se na server je vyžadován")]
    public DateTime ConectingToServer { get; set; } = new(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 18, 30, 0);
    
    [Required(ErrorMessage = "Čas začátku mise je vyžadován")]
    public DateTime Start { get; set; } = new(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 19, 00, 0);
    
    [Url]
    public string? ModpackUrl { get; set; }
    
    [Required(ErrorMessage = "Uvedení druhu mise je vyžadováno")]
    public Guid? CategoryId { get; set; }
    public Guid? ModpackTypeId { get; set; }

    public Mission ToMission(Guid? missionId = null)
    {
        if (!Date.HasValue || Name is null || SlottingSheetUrl is null)
            throw new InvalidDataException();

        var start = Date.Value.Date + Start.TimeOfDay;
        var connecting =Date.Value.Date + ConectingToServer.TimeOfDay;        
        
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