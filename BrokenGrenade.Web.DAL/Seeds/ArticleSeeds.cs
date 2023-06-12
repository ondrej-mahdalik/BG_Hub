using BrokenGrenade.Web.DAL.Entities;
namespace BrokenGrenade.Web.DAL.Seeds;

public static class ArticleSeeds
{
    public static readonly ArticleEntity Article1 = new("Tvorba misí")
    {
        Content = "<h1>Sed semper dictum nunc</h1><p><strong>Sed rhoncus odio congue vitae.</strong> <span style=\"color: rgb(255, 0, 0); text-decoration: inherit;\">Aliquam</span> id malesuada leo, eget elementum lacus. " +
                  "Vivamus nec ullamcorper massa, quis varius eros. Sed congue erat id pellentesque gravida. Pellentesque feugiat ipsum ut tortor varius, non consequat lectus tincidunt. Duis eleifend lectus vitae laoreet bibendum. " +
                  "Nullam eros nisl, fringilla interdum rutrum et, commodo nec sem.</p><p><br></p><h2>In pellentesque lacinia augue</h2><p>Vel fringilla leo convallis vitae. Proin ut mollis arcu. Vestibulum ante " +
                  "<span style=\"color: rgb(255, 0, 0); text-decoration: inherit;\">ipsum</span> primis in faucibus orci luctus et ultrices posuere cubilia <span style=\"color: rgb(0, 0, 0); text-decoration: inherit;\"><span " +
                  "style=\"background-color: rgb(255, 255, 0);\">curae</span></span>; Curabitur sit amet ullamcorper sapien, non laoreet nibh. Cras non purus dictum, pellentesque tellus ut, convallis ex. Nullam consequat nibh" +
                  " eget faucibus volutpat. Mauris suscipit sapien eget scelerisque tristique. Nullam placerat nibh vitae dui ultrices, in imperdiet turpis semper. Ut vitae dignissim ex, a tristique lorem. Proin sit amet maximus" +
                  " lectus, suscipit consequat nisl. Fusce est diam, bibendum consequat sodales non, varius sit amet erat. Pellentesque tortor risus, suscipit maximus dignissim a, cursus et urna. Quisque rutrum dui eget suscipit" +
                  " efficitur.</p><table class=\"e-rte-table\" style=\"width: 100%; min-width: 0px;\"><tbody><tr><td style=\"width: 33.3333%;\" class=\"\"><span style=\"color: rgb(217, 226, 243); text-decoration: inherit;\">" +
                  "<strong>Ultrices</strong></span></td><td style=\"width: 33.3333%;\" class=\"\"><span style=\"color: rgb(217, 226, 243); text-decoration: inherit;\"><strong>Imperdiet</strong></span></td><td style=\"width: " +
                  "33.3333%;\" class=\"\"><strong><span style=\"color: rgb(217, 226, 243); text-decoration: inherit;\">Turpi</span><span style=\"color: rgb(217, 226, 243); text-decoration: inherit;\">s</span></strong></td>" +
                  "</tr><tr><td style=\"width: 33.3333%;\" class=\"\">Semper</td><td style=\"width: 33.3333%;\">Dignissim</td><td style=\"width: 33.3333%;\">Tristique</td></tr></tbody></table><p><br></p>",
        Excerpt = "Sed semper dictum nunc, sed rhoncus odio congue vitae. Aliquam id malesuada leo, eget elementum lacus. Vivamus nec ullamcorper massa, quis varius eros. Sed congue erat id pellentesque gravida.",
        Id = new Guid("8943160F-2371-493D-93B5-3404AD2D06A6"), CreatorId = UserSeeds.Borek.Id
    };

    public static async Task SeedAsync(BrokenGrenadeDbContext dbContext)
    {
        await dbContext.ArticleEntities.AddRangeAsync(Article1);
    }
}
