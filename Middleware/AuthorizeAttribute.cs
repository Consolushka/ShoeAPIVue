using System;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Middleware
{
    public class UserCheck
    {
        public string IsActiveUserExistingUser(User user)
        {
            if (user == null)
            {
                return "Unauthorized";
            }

            if (user.IsActive == false)
            {
                // not logged in
                return "Your account is inactive";
            }

            return null;
        }
        
        public string IsAdmin(User user)
        {
            var isExists = IsActiveUserExistingUser(user); 
            if (isExists != null)
            {
                return isExists;
            }

            if (user.IsAdmin)
            {
                return null;
            }

            return "You dont have needed rights";
        }
    }
    
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute: Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var answer = new UserCheck().IsActiveUserExistingUser((User)context.HttpContext.Items["User"]);
            
            if (answer == null)
            {
                return;
            }
            
            context.Result = new JsonResult(new { message = answer })
                { StatusCode = StatusCodes.Status401Unauthorized };
        }
    }

    public class AdminAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var answer = new UserCheck().IsAdmin((User)context.HttpContext.Items["User"]);
            
            if (answer == null)
            {
                return;
            }
            
            context.Result = new JsonResult(new { message = answer })
                { StatusCode = StatusCodes.Status401Unauthorized };
        }
    }
}