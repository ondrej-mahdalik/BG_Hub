namespace BrokenGrenade.Web.DAL.Entities;

public class PunishmentEntity : EntityBase
{
    public string Reason { get; set; }
    public string Punishment { get; set; }
    public DateTime IssuedOn { get; set; }
    public DateTime? ExpiresOn { get; set; }
    
    public Guid? IssuedById { get; set; }
    public UserEntity? IssuedBy { get; init; }
    
    public Guid UserId { get; set; }
    public UserEntity? User { get; init; }

    public PunishmentEntity(string reason, string punishment, DateTime issuedOn, DateTime? expiresOn = null)
    {
        Reason = reason;
        Punishment = punishment;
        IssuedOn = issuedOn;
        ExpiresOn = expiresOn;
    }
}
