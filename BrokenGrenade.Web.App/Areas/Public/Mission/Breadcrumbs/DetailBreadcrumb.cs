using Supercode.Blazor.BreadcrumbNavigation.Services;

namespace BrokenGrenade.Web.App.Areas.Public.Mission.Breadcrumbs;

public class DetailBreadcrumb : Breadcrumb
{
    public override void Configure(BreadcrumbBuilder builder)
        => builder.Link("Mise", string.Empty);
}