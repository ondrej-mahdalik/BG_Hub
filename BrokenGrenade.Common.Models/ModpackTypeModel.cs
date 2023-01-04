using System.ComponentModel.DataAnnotations;

namespace BrokenGrenade.Common.Models
{
    public class ModpackTypeModel : ModelBase
    {
        public ModpackTypeModel(string name)
        {
            Name = name;
        }
        
        [Required]
        [Display(Name = "Název")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "{0} musí mít minimálně {2} znaky a maximálně {1} znaků.")]
        public string Name { get; set; }
    }
}
