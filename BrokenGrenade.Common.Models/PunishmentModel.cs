using System.ComponentModel.DataAnnotations;
namespace BrokenGrenade.Common.Models;

public class PunishmentModel : ModelBase
{
    [Required]
    public string Reason { get; set; }
    
    [Required]
    public string Punishment { get; set; }
    
    [Required]
    public DateTime IssuedOn { get; set; } = DateTime.Now;
    public DateTime? ExpiresOn { get; set; }
    
    [Required]
    public Guid? IssuedById { get; set; }
    public UserModel? IssuedBy { get; set; }
    
    [Required]
    public Guid? UserId { get; set; }
    public UserModel? User { get; set; }

    public PunishmentModel(string reason, string punishment, DateTime? expiresOn = null)
    {
        Reason = reason;
        Punishment = punishment;
        ExpiresOn = expiresOn;
    }

    public PunishmentModel()
    {
        Reason = string.Empty;
        Punishment = string.Empty;
    }
}
