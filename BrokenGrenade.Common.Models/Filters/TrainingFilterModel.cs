namespace BrokenGrenade.Common.Models.Filters;

public class TrainingFilterModel
{
    public string? Name { get; set; }
    public string? Creator { get; set; }
    public Guid? CreatorId { get; set; }
    public DateTime? Date { get; set; }
}
