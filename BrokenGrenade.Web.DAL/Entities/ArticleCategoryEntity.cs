namespace BrokenGrenade.Web.DAL.Entities;

public class ArticleCategoryEntity : EntityBase
{
    public string Name { get; set; }
    
    public Guid? ParentId { get; set; }
    public ArticleCategoryEntity? Parent { get; init; }
    public ICollection<ArticleCategoryEntity> Children { get; init; } = new List<ArticleCategoryEntity>();
    
    public ICollection<ArticleEntity> Articles { get; init; } = new List<ArticleEntity>();

    public ArticleCategoryEntity(string name)
    {
        Name = name;
    }
}