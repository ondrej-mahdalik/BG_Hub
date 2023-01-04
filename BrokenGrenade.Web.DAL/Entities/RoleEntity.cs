using Microsoft.AspNetCore.Identity;

namespace BrokenGrenade.Web.DAL.Entities;

public sealed class RoleEntity : IdentityRole<Guid>, ICloneable
{
    public RoleEntity(string name) : base(name)
    {
        Id = Guid.NewGuid();
    }

    public object Clone()
    {
        return MemberwiseClone();
    }
}