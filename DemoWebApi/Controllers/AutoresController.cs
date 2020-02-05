using DemoWebApi.Contexts;
using DemoWebApi.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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
        private readonly ApplicationDbContext _context;
        public AutoresController(ApplicationDbContext Context)
        {
            _context = Context;
        }

        // GET: api/autores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Autor>>> GetAutores()
        {
            return await _context.Autores.Include(autor => autor.Libros).ToListAsync();
        }

        // GET: api/autores/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Autor>> GetAutor(int Id)
        {
            Autor Autor = await _context.Autores.Include(autor => autor.Libros).FirstOrDefaultAsync(autor => autor.Id == Id);

            if (Autor == null)
            {
                return NotFound();
            }

            return Autor;
        }

        // POST: api/autores
        [HttpPost]
        public async Task<ActionResult<Autor>> PostAutor([FromBody] Autor Autor)
        {
            //Esto ya no es necesario desde la versión 2.1 en adelante
            /*if (!ModelState.IsValid)
            {
                return NotFound();
            }*/

            _context.Autores.Add(Autor);
            await _context.SaveChangesAsync();
            return new CreatedAtRouteResult("GetAutor", new { id = Autor.Id }, Autor);
        }

        // PUT: api/autores/5
        [HttpPut("{id}")]
        public async Task<ActionResult> PutAutor(int Id, [FromBody] Autor Autor)
        {
            if (Id != Autor.Id)
            {
                return BadRequest();
            }

            _context.Entry(Autor).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AutorExists(Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            
            return Ok();
        }

        // DELETE: api/autores/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Autor>> DeleteAutor(int Id)
        {
            Autor Autor = await _context.Autores.FindAsync(Id);

            if (Autor == null)
            {
                return NotFound();
            }

            _context.Autores.Remove(Autor);
            await _context.SaveChangesAsync();
            return Autor;
        }

        private bool AutorExists(int Id)
        {
            return _context.Autores.Any(autor => autor.Id == Id);
        }

    }
}
