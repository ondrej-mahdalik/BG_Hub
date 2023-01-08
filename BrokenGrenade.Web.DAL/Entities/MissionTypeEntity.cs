namespace BrokenGrenade.Web.DAL.Entities;

public class MissionTypeEntity : EntityBase
{
    public MissionTypeEntity(string name, string? note = null)
    {
        Name = name;
        Note = note;
    }
    
    public string Name { get; set; }
    public string? Note { get; set; }
    
    public ICollection<MissionEntity> Missions { get; init; } = new List<MissionEntity>();
}