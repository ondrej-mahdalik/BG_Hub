using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace BrokenGrenade.Data
{
    public class User : IdentityUser
    {
        public IList<Mission> Missions { get; set; } = new List<Mission>();
    }
}
