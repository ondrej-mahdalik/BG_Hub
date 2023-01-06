using BrokenGrenade.Web.App.Extensions.AuthorizationFilters;
using Microsoft.AspNetCore.Mvc;

namespace BrokenGrenade.Web.App.Extensions.Attributes;

/// <summary>
/// Specifies that the class or method that this attribute is applied to requires 
/// authorization based on user passing any one policy in the provided list of policies.
/// </summary>
public class AuthorizeAnyPolicyAttribute : TypeFilterAttribute
{
    /// <summary>
    /// Initializes a new instance of the AuthorizeAnyPolicyAttribute class.
    /// </summary>
    /// <param name="policies">A comma delimited list of policies that are allowed to access the resource.</param>
    public AuthorizeAnyPolicyAttribute(string policies) : base(typeof(AuthorizeAnyPolicyFilter))
    {
        Arguments = new object[] { policies };
    }
}