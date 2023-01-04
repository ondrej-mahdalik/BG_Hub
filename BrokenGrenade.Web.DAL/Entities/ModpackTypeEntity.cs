namespace BrokenGrenade.Web.DAL.Entities;

public class ModpackTypeEntity : EntityBase
{
    public ModpackTypeEntity(string name)
    {
        Name = name;
    }
    
    public string Name { get; set; }
    
    public ICollection<MissionEntity> Missions { get; set; } = new List<MissionEntity>();
}