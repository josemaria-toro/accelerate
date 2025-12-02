using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace Zetatech.Accelerate.AspNet.Abstractions;

/// <summary>
/// Represents a base class for implementing custom authorization filters in ASP.NET Core MVC.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
public class AuthorizationFilterAttribute : Attribute, IAuthorizationFilter
{
    /// <summary>
    /// Called during the authorization phase to determine whether the current request is authorized.
    /// </summary>
    /// <param name="context">
    /// The context for the authorization filter.
    /// </param>
    public virtual void OnAuthorization(AuthorizationFilterContext context)
    {
    }
}
