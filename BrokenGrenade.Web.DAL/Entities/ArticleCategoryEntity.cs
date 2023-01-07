namespace BrokenGrenade.Web.DAL.Entities;

public class ArticleCategoryEntity : EntityBase
{
    public string Name { get; set; }
    
    public Guid? ParentId { get; set; }
    public ArticleCategoryEntity? Parent { get; set; }
    public ICollection<ArticleCategoryEntity> Children { get; set; } = new List<ArticleCategoryEntity>();
    
    public ICollection<ArticleEntity> Articles { get; set; } = new List<ArticleEntity>();

    public ArticleCategoryEntity(string name)
    {
        Name = name;
    }
}