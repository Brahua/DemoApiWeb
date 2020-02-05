using DemoWebApi.Contexts;
using DemoWebApi.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutoresController : ControllerBase
    {
        private readonly ApplicationDbContext Context;
        public AutoresController(ApplicationDbContext Context)
        {
            this.Context = Context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Autor>> Get()
        {
            return Context.Autores.Include(autor => autor.Libros).ToList();
        }

        [HttpGet("{id}", Name = "ObtenerAutor")]
        public ActionResult<Autor> Get(int Id)
        {
            Autor Autor = this.Context.Autores.Include(autor => autor.Libros).FirstOrDefault(autor => autor.Id == Id);

            if (Autor == null)
            {
                return NotFound();
            }

            return Autor;
        }

        [HttpPost]
        public ActionResult<Autor> Post([FromBody] Autor Autor)
        {
            //Esto ya no es necesario desde la versión 2.1 en adelante
            /*if (!ModelState.IsValid)
            {
                return NotFound();
            }*/

            this.Context.Autores.Add(Autor);
            this.Context.SaveChanges();
            return new CreatedAtRouteResult("ObtenerAutor", new { id = Autor.Id }, Autor);
        }

        [HttpPut("{id}")]
        public ActionResult Put(int Id, [FromBody] Autor Autor)
        {
            if (Id != Autor.Id)
            {
                return BadRequest();
            }

            this.Context.Entry(Autor).State = EntityState.Modified;
            this.Context.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult<Autor> Delete(int Id)
        {
            Autor Autor = this.Context.Autores.FirstOrDefault(autor => autor.Id == Id);
            if (Autor == null)
            {
                return NotFound();
            }

            this.Context.Autores.Remove(Autor);
            this.Context.SaveChanges();
            return Autor;
        }

    }
}
