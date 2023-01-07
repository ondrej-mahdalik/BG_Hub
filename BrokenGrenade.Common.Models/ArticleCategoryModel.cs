namespace BrokenGrenade.Common.Models;

public class ArticleCategoryModel : ModelBase
{
    public string Name { get; set; }
    
    public ArticleCategoryModel? Parent { get; set; }
    public Guid? ParentId { get; set; }
    public IList<ArticleCategoryModel> Children { get; set; } = new List<ArticleCategoryModel>();
    
    public IList<ArticleModel> Articles { get; set; } = new List<ArticleModel>();

    public ArticleCategoryModel(string name)
    {
        Name = name;
    }

    public ArticleCategoryModel()
    {
        Name = string.Empty;
    }
}