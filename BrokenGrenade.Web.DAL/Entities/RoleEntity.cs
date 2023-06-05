using Microsoft.AspNetCore.Identity;

namespace BrokenGrenade.Web.DAL.Entities;

public sealed class RoleEntity : IdentityRole<Guid>, ICloneable
{
    public string? ColorHex { get; set; }

    public RoleEntity(string name) : base(name)
    {
        Id = Guid.NewGuid();
    }

    public int Order { get; set; } = 0;

    public object Clone()
    {
        return MemberwiseClone();
    }
}