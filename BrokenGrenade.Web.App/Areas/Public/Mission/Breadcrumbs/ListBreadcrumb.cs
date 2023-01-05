using Supercode.Blazor.BreadcrumbNavigation.Services;

namespace BrokenGrenade.Web.App.Areas.Public.Mission.Breadcrumbs;

public class ListBreadcrumb : Breadcrumb
{
    public override void Configure(BreadcrumbBuilder builder)
        => builder.Link("Seznam misí", "/mission/list");
}