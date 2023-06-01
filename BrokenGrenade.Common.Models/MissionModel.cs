using System.ComponentModel.DataAnnotations;

namespace BrokenGrenade.Common.Models
{
    public class MissionModel : ModelBase, IValidatableObject
    {
        public MissionModel(string name, DateTime missionStartDate)
        {
            Name = name;
            MissionStartDate = missionStartDate;
        }

        public MissionModel()
        {

        }

        [Required]
        [Display(Name = "Název mise")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "{0} musí mít minimálně {2} znaky a maximálně {1} znaků.")]
        public string? Name { get; set; }
        
        [Display(Name = "Odkaz na slotování")]
        [Url]
        [Required]
        public string? SlottingSheetUrl { get; set; }
        
        [Display(Name = "Odkaz ke stažení modpacku")]
        [Url]
        public string? ModpackUrl { get; set; }

        [Required]
        [Display(Name = "Začátek mise")]
        public DateTime? MissionStartDate { get; set; }
        
        public UserModel? Creator { get; set; }
        public Guid? CreatorId { get; set; }
        
        public MissionTypeModel? MissionType { get; set; }
        public Guid? MissionTypeId { get; set; }
        
        public ModpackTypeModel? ModpackType { get; set; }
        public Guid? ModpackTypeId { get; set; }
        
        public ulong? DiscordMessageId { get; set; }
        
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            // Validate mission start
            if (MissionStartDate < DateTime.Now)
                yield return new ValidationResult("Začátek mise nemůže být v minulosti.",
                    new[] { nameof(MissionStartDate) });
        }
    }
}
