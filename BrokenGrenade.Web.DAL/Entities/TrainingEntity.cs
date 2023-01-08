namespace BrokenGrenade.Web.DAL.Entities;

public class TrainingEntity : EntityBase
{
    public string Name { get; set; }
    public string? Note { get; set; }
    public DateTime Date { get; set; }
    
    public UserEntity? Creator { get; init; }
    public Guid CreatorId { get; set; }
    
    public ICollection<UserIsParticipatingTrainingEntity> Participants { get; init; } = new List<UserIsParticipatingTrainingEntity>();

    public TrainingEntity(string name, DateTime date, string? note = null)
    {
        Name = name;
        Date = date;
        Note = note;
    }
}