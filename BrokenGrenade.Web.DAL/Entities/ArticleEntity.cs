namespace BrokenGrenade.Web.DAL.Entities;

public class ArticleEntity : EntityBase
{
    public string Name { get; set; }
    public string? Content { get; set; }
    
    public ArticleCategoryEntity? Category { get; init; }
    public Guid? CategoryId { get; set; }

    public ArticleEntity(string name, string? content = null)
    {
        Name = name;
        Content = content;
    }
}