using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMaze.Services;

namespace WebMaze.Controllers.CustomAttribute.Life
{
    public class IsFireman : IsPoliceman
    {
        //private string roleName;

        public IsFireman()
        {
            base.roleName = "Fireman";
        }
    }
}
