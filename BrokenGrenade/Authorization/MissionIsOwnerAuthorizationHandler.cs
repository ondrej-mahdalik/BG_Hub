using BrokenGrenade.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;

namespace BrokenGrenade.Authorization;

public class MissionIsOwnerAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement, Mission>
{
    private UserManager<User> _userManager;

    public MissionIsOwnerAuthorizationHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }
    
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement,
        Mission resource)
    {
        if (resource.AuthorId == _userManager.GetUserId(context.User))
            context.Succeed(requirement);
        
        return Task.CompletedTask;
    }
}