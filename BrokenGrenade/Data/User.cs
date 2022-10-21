using Microsoft.AspNetCore.Identity;

namespace BrokenGrenade.Data
{
    public class User : IdentityUser<Guid>
    {
        public IList<Mission> Missions { get; set; } = new List<Mission>();
    }
}
