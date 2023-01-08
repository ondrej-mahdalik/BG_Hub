namespace BrokenGrenade.Common.Models;

public class UserIsParticipatingTrainingModel : ModelBase
{
    public UserModel? User { get; set; }
    public Guid UserId { get; set; }
    
    public TrainingModel? Training { get; set; }
    public Guid TrainingId { get; set; }
    
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}