using Supercode.Blazor.BreadcrumbNavigation.Services;

namespace BrokenGrenade.Web.App.Pages.Breadcrumbs;

public class IndexBreadcrumb : RootBreadcrumb
{
    public override void Configure(BreadcrumbBuilder builder)
        => builder.Link("Domů", String.Empty);
}