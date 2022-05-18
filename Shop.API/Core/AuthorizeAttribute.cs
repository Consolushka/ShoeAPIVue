using System;
using Shop.Data.Models;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Shop.API.Core
{
    public class UserCheck
    {
        public string IsActiveUserExistingUser(User user)
        {
            if (user == null)
            {
                throw new Exception("You are Unauthorized");
            }

            if (user.IsActive == false)
            {
                // not logged in
                throw new Exception($"Email {user.Email} is unconfirmed");
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

            throw new Exception($"User with Email {user.Email} don't have needed rights");
        }
    }
    
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute: Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var answer = new UserCheck().IsActiveUserExistingUser((User)context.HttpContext.Items["User"]);
        }
    }

    public class AdminAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var answer = new UserCheck().IsAdmin((User)context.HttpContext.Items["User"]);
        }
    }
}