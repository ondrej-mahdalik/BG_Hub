namespace BrokenGrenade.Web.DAL.Entities;

public abstract class EntityBase : IEntity, ICloneable
{
    public Guid Id { get; init; } = Guid.NewGuid();
    
    public object Clone()
    {
        return MemberwiseClone();
    }
}