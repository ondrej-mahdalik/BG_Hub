using BrokenGrenade.Data;

namespace BrokenGrenade.Models;

public class MissionModel
{
    public string Title { get; set; }
    public string Url { get; set; }
    public DateTime Start { get; set; }

    public MissionModel(string title, string url, DateTime start)
    {
        Title = title;
        Url = url;
        Start = start;
    }

    public static MissionModel FromEntity(Mission entity)
    {
        return new MissionModel(entity.Name, $"/Mission/{entity.Id}", entity.Start);
    }
}