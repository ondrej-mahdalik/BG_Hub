namespace BrokenGrenade.Web.DAL.Entities;

public class MissionEntity : EntityBase
{
    public string Name { get; set; }
    public string? SlottingSheetUrl { get; set; }
    public string? ModpackUrl { get; set; }
    public DateTime? LeaderBriefingDate { get; set; }
    public DateTime ConnectingToServerDate { get; set; }
    public DateTime MissionStartDate { get; set; }
    
    public UserEntity? Creator { get; init; }
    public Guid? CreatorId { get; set; }
    
    public MissionTypeEntity? MissionType { get; init; }
    public Guid? MissionTypeId { get; set; }
    
    public ModpackTypeEntity? ModpackType { get; init; }
    public Guid? ModpackTypeId { get; set; }
    
    public ulong? DiscordMessageId { get; set; }
    
    public MissionEntity(string name, DateTime connectingToServerDate, DateTime missionStartDate)
    {
        Name = name;
        ConnectingToServerDate = connectingToServerDate;
        MissionStartDate = missionStartDate;
    }
}