using System.Collections;
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
    public ApplicationEntity? Application { get; init; }
    
    public ICollection<MissionEntity> MissionsCreated { get; init; } = new List<MissionEntity>();
    public ICollection<ApplicationEntity> ApplicationsEdited { get; init; } = new List<ApplicationEntity>();
    public ICollection<UserIsParticipatingTrainingEntity> TrainingsParticipating { get; init; } = new List<UserIsParticipatingTrainingEntity>();

    public object Clone()
    {
        return MemberwiseClone();
    }
}