using Microsoft.AspNetCore.Identity;

namespace BrokenGrenade.Web.DAL.Entities;

public sealed class UserEntity : IdentityUser<Guid>, ICloneable
{
    public UserEntity(string nickname,
        string email)
    {
        Id = Guid.NewGuid();
        Nickname = nickname;
        Email = email;
        UserName = email;
    }

    public UserEntity()
    {
        Id = Guid.NewGuid();
        Nickname = string.Empty;
        Email = string.Empty;
        UserName = string.Empty;
    }
    
    public string Nickname { get; set; }
    
    public Guid? ApplicationId { get; set; }
    public ApplicationEntity? Application { get; set; }
    
    public ICollection<MissionEntity> MissionsCreated { get; set; } = new List<MissionEntity>();
    public ICollection<ApplicationEntity> ApplicationsEdited { get; set; } = new List<ApplicationEntity>();

    public object Clone()
    {
        return MemberwiseClone();
    }
}