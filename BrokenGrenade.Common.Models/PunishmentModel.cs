using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace BrokenGrenade.Common.Models;

[JsonObject(MemberSerialization.OptIn)]
public class PunishmentModel : ModelBase
{
    [Required]
    [JsonProperty]
    public string Reason { get; set; }
    
    [Required]
    [JsonProperty]
    public string Punishment { get; set; }
    
    [Required]
    [JsonProperty]
    public DateTime IssuedOn { get; set; } = DateTime.Now;
    
    [JsonProperty]
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
