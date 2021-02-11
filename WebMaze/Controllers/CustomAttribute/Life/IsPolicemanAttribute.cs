using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMaze.Services;

namespace WebMaze.Controllers.CustomAttribute.Life
{
    public class IsPoliceman : BaseAuthAttribute
    {
        public IsPoliceman()
        {
            base.roleName = "Policeman";
        }

    }
}
