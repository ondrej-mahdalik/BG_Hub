namespace BrokenGrenade.Common.Models;

public class ArticleModel : ModelBase
{
    public string Name { get; set; }
    public string? Content { get; set; }
    
    public Guid? CategoryId { get; set; }
    public ArticleCategoryModel? Category { get; set; }
    
    public ArticleModel(string name, string? content = null)
    {
        Name = name;
        Content = content;
    }
    
    public ArticleModel()
    {
        Name = string.Empty;
    }
}