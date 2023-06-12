namespace BrokenGrenade.Web.DAL.Entities;

public class ArticleEntity : EntityBase
{
    public string Name { get; set; }
    public string Content { get; set; }
    public string Excerpt { get; set; }
    public string? ImageUrl { get; set; }
    
    public UserEntity? Creator { get; init; }
    public Guid? CreatorId { get; set; }

    public ArticleEntity(string name, string content = "")
    {
        Name = name;
        Content = content;
    }
}