using System.ComponentModel.DataAnnotations;

namespace BrokenGrenade.Common.Models
{
    public class MissionTypeModel : ModelBase
    {
        public MissionTypeModel(string name, string? note = null)
        {
            Name = name;
            Note = note;
        }
        
        [Required]
        [Display(Name = "Název")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "{0} musí mít minimálně {2} znaky a maximálně {1} znaků.")]
        public string Name { get; set; }
        
        [Display(Name = "Poznámka", Description = "Zobrazí se v detailu mise")]
        [StringLength(200, ErrorMessage = "{0} může mít maximálně {1} znaků.")]
        public string? Note { get; set; }
    }
}
