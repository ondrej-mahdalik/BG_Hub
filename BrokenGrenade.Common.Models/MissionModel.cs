using System.ComponentModel.DataAnnotations;

namespace BrokenGrenade.Common.Models
{
    public class MissionModel : ModelBase, IValidatableObject
    {
        public MissionModel(string name, DateTime connectingToServerDate, DateTime missionStartDate)
        {
            Name = name;
            ConnectingToServerDate = connectingToServerDate;
            MissionStartDate = missionStartDate;
        }
        
        [Required]
        [Display(Name = "Název mise")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "{0} musí mít minimálně {2} znaky a maximálně {1} znaků.")]
        public string Name { get; set; }
        
        [Display(Name = "Odkaz na slotování")]
        [Url]
        public string? SlottingSheetUrl { get; set; }
        
        [Display(Name = "Odkaz ke stažení modpacku")]
        [Url]
        public string? ModpackUrl { get; set; }
        
        [Display(Name = "Velitelský briefing")]
        public DateTime? LeaderBriefingDate { get; set; }
        
        [Display(Name = "Připojování na server")]
        public DateTime ConnectingToServerDate { get; set; }
        
        [Display(Name = "Začátek mise")]
        public DateTime MissionStartDate { get; set; }
        
        public UserModel? Creator { get; set; }
        public Guid? CreatorId { get; set; }
        
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            // Validate leader briefing
            if (LeaderBriefingDate < DateTime.Now)
                yield return new ValidationResult("Velitelský briefing nemůže být v minulosti.",
                    new[] { nameof(LeaderBriefingDate) });
            
            if (LeaderBriefingDate > MissionStartDate)
                yield return new ValidationResult("Velitelský briefing nemůže být po začátku mise.",
                    new[] { nameof(LeaderBriefingDate) });
            
            // Validate connecting to server
            if (ConnectingToServerDate < DateTime.Now)
                yield return new ValidationResult("Připojování na server nemůže být v minulosti.",
                    new[] { nameof(ConnectingToServerDate) });
            
            if (ConnectingToServerDate > MissionStartDate)
                yield return new ValidationResult("Připojování na server nemůže být po začátku mise.",
                    new[] { nameof(ConnectingToServerDate) });
            
            // Validate mission start
            if (MissionStartDate < DateTime.Now)
                yield return new ValidationResult("Začátek mise nemůže být v minulosti.",
                    new[] { nameof(MissionStartDate) });
        }
    }
}
