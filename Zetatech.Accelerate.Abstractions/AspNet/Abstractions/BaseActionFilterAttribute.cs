using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace Zetatech.Accelerate.AspNet.Abstractions;

/// <summary>
/// Represents a base class for implementing custom action filters in ASP.NET Core MVC.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
public abstract class BaseActionFilterAttribute : Attribute, IActionFilter
{
    /// <summary>
    /// Called after the action method is executed.
    /// </summary>
    /// <param name="context">
    /// The context for the executed action.
    /// </param>
    public virtual void OnActionExecuted(ActionExecutedContext context)
    {
    }
    /// <summary>
    /// Called before the action method is executed.
    /// </summary>
    /// <param name="context">
    /// The context for the executing action.
    /// </param>
    public virtual void OnActionExecuting(ActionExecutingContext context)
    {
    }
}
