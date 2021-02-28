using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMaze.Services;

namespace WebMaze.Models.CustomAttribute.Life
{
    public class BaseAuthAttribute : ActionFilterAttribute
    {
        protected string roleName;
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var lifeService = context.HttpContext.RequestServices
                .GetService(typeof(LifeService)) as LifeService;
            var user = lifeService.GetCurrentUser();

            // if user is not Authenticated or does not belong to required "roleName"
            if (user == null || user.Roles.All(r => r.Name != this.roleName))
            {
                context.Result = new ForbidResult();
            }

        }
    }
}
