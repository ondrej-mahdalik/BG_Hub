using System.ComponentModel.DataAnnotations;

namespace BrokenGrenade.Common.Models
{
    public class UserModel : ModelBase
    {
        public UserModel(string nickname, string email)
        {
            Email = email;
            Nickname = nickname;
        }
        
        [Required]
        [Display(Name = "Emailová adresa")]
        [EmailAddress]
        public string Email { get; set; }
        
        [Required]
        [Display(Name = "Přezdívka")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "{0} musí mít minimálně {2} znaky a maximálně {1} znaků.")]
        public string Nickname { get; set; }
        
        public Guid? ApplicationId { get; set; }
        public ApplicationModel? Application { get; set; }

        public IList<RoleModel> Roles { get; set; } = new List<RoleModel>();
        public IList<MissionModel> MissionsCreated { get; set; } = new List<MissionModel>();
        public IList<UserIsParticipatingTrainingModel> TrainingsParticipating { get; set; } = new List<UserIsParticipatingTrainingModel>();
    }
}