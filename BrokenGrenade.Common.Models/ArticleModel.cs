using System.ComponentModel.DataAnnotations;
namespace BrokenGrenade.Common.Models;

public class ArticleModel : ModelBase
{
    [Required]
    public string Name { get; set; }
    
    [Required]
    public string Content { get; set; }
    
    [Required]
    [StringLength(100, MinimumLength = 10)]
    public string Excerpt { get; set; }
    
    [Url]
    public string? ImageUrl { get; set; }
    
    public UserModel? Creator { get; init; }
    public Guid? CreatorId { get; set; }
    
    public ArticleModel(string name, string content = "")
    {
        Name = name;
        Content = content;
    }
    
    public ArticleModel()
    {
        Name = string.Empty;
        Content = string.Empty;
    }
}