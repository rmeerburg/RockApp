using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Mvc;
using RockApp.Models;
using RockApp.Services;

namespace RockApp.Controllers
{
    public abstract class EntityController : Controller
    {
        public override Task OnActionExecutionAsync(Microsoft.AspNetCore.Mvc.Filters.ActionExecutingContext context, Microsoft.AspNetCore.Mvc.Filters.ActionExecutionDelegate next)
        {
            if (!ModelState.IsValid)
                context.Result = new BadRequestObjectResult(context.ModelState);
            else
                return next();
            return Task.CompletedTask;
        }
    }
}
