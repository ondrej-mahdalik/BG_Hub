using BrokenGrenade.Data;

namespace BrokenGrenade.Models;

public class MissionApiModel
{
    public string Title { get; set; }
    public string Url { get; set; }
    public DateTime Start { get; set; }

    public MissionApiModel(string title, string url, DateTime start)
    {
        Title = title;
        Url = url;
        Start = start;
    }

    public static MissionApiModel FromEntity(Mission entity)
    {
        return new MissionApiModel(entity.Name, $"/Mission/{entity.Id}", entity.Start);
    }
}