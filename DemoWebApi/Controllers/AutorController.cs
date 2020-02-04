using DemoWebApi.Contexts;
using DemoWebApi.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutorController : ControllerBase
    {
        private readonly ApplicationDbContext Context;
        public AutorController(ApplicationDbContext Context) 
        {
            this.Context = Context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Autor>> Get()
        {
            return Context.Autor.ToList();
        }
    }
}
