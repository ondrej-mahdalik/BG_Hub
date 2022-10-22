namespace BrokenGrenade.Data;

public class ModpackType
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public ModpackType(string name)
    {
        Name = name;
    }

    public IList<Mission> Missions { get; set; } = new List<Mission>();
}