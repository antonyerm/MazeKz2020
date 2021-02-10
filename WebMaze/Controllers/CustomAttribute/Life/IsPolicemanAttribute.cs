using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMaze.Services;

namespace WebMaze.Controllers.CustomAttribute.Life
{
    public class IsPoliceman : ActionFilterAttribute
    {
        protected string roleName;

        public IsPoliceman()
        {
            this.roleName = "Policeman";
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var userService = context.HttpContext.RequestServices
                .GetService(typeof(UserService)) as UserService;
            var user = userService.GetCurrentUser();

            // if not Authenticated or is not a Policeman
            if (user == null || user.Roles.All(r => r.Name != this.roleName))
            {
                context.Result = new ForbidResult();
            }

        }
    }
}
