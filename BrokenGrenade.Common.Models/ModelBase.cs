namespace BrokenGrenade.Common.Models;

public class ModelBase : IModel
{
    public Guid Id { get; init; } = Guid.NewGuid();
}