using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

namespace BrokenGrenade.Web.App.Extensions;

public static class GetIdFromAuthenticationStateExtension
{
    public static Guid? GetUserId(this AuthenticationState authenticationState)
    {
        var userId =
            authenticationState.User.FindFirstValue(
                "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");
        return userId is null ? null : Guid.Parse(userId);
    }
}