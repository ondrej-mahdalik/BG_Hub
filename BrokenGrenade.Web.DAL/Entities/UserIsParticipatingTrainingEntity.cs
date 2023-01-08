namespace BrokenGrenade.Web.DAL.Entities;

public class UserIsParticipatingTrainingEntity : EntityBase
{
    public UserEntity? User { get; init; }
    public Guid UserId { get; set; }
    public TrainingEntity? Training { get; init; }
    public Guid TrainingId { get; set; }
    
    public DateTime Timestamp { get; set; } = DateTime.Now;
}