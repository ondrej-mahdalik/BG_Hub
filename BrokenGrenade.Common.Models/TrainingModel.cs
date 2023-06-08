using BrokenGrenade.Common.Enums;
namespace BrokenGrenade.Common.Models;

public class TrainingModel : ModelBase
{
    public string Name { get; set; }
    public string? Note { get; set; }
    public DateTime Date { get; set; }
    
    public UserModel? Creator { get; set; }
    public Guid? CreatorId { get; set; }

    public ulong? DiscordMessageId { get; set; }

    public TrainingModel(string name, DateTime date, string? note = null)
    {
        Name = name;
        Date = date;
        Note = note;
    }

    public TrainingModel()
    {
        Name = string.Empty;
        Date = DateTime.Today;
    }
}