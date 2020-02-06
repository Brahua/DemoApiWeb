using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoWebApi.Helpers
{
    public class FiltroExcepcion: ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            
        }

    }
}
