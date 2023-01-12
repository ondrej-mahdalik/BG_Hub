using System.ComponentModel.DataAnnotations;

namespace BrokenGrenade.Common.Models
{
    public class RoleModel : ModelBase
    {
        public RoleModel(string name)
        {
            Name = name;
        }
        
        [Required]
        [Display(Name = "Název")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "{0} musí mít minimálně {2} znaky a maximálně {1} znaků.")]
        public string Name { get; set; }
        
        [Required]
        [Display(Name = "Priorita")]
        [Range(0, int.MaxValue, ErrorMessage = "{0} musí být kladné číslo.")]

        public int Priority { get; set; } = 0;
        
        public int UserCount { get; set; }
        
        public bool CreateMissions { get; set; }
        public bool CreateTrainings { get; set; }
        public bool ManageUsers { get; set; }
        public bool ManageRoles { get; set; }
        public bool ManageMissions { get; set; }
        public bool ManageTrainings { get; set; }
        public bool ManageMissionTypes { get; set; }
        public bool ManageModpackTypes { get; set; }
        public bool ManageApplications { get; set; }
    }
}
