﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;

namespace StorageManagementApp.Contracts.Guards
{
    public class PrivateGuardAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.HttpContext.User?.Identity?.IsAuthenticated ?? false)
            {
                //context.Result = new RedirectToRouteResult(
                //    new RouteValueDictionary(
                //        new { controller = "User", action = "Login" })
                //    );
            }
            else
            {
                context.Result = new RedirectToRouteResult(
                    new RouteValueDictionary(
                        new { controller = "Product", action = "Index"})
                    );
            }
        }
    }
}
