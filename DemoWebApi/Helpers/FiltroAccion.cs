using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoWebApi.Helpers
{
    public class FiltroAccion : IActionFilter
    {
        private readonly ILogger<FiltroAccion> Logger;
        public FiltroAccion(ILogger<FiltroAccion> logger)
        {
            this.Logger = logger;
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            this.Logger.LogError("OnActionExecuted");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            this.Logger.LogError("OnActionExecuting");
        }
    }
}
