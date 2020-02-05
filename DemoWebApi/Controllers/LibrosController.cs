using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DemoWebApi.Contexts;
using DemoWebApi.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace DemoWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibrosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public LibrosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/libros
        [HttpGet]
        // GET: /libros
        [HttpGet("/libros")]
        public async Task<ActionResult<IEnumerable<Libro>>> GetLibros()
        {
            return await _context.Libros.Include(libro => libro.Autor).ToListAsync();
        }

        // GET: api/libros/primer
        [HttpGet("primer")]
        public async Task<ActionResult<Libro>> GetPrimerLibro()
        {
            return await _context.Libros.Include(libro => libro.Autor).FirstAsync();
        }

        // GET: api/libros/5 o api/libros/5/**
        [HttpGet("{id}/{param2?}")]
        //[HttpGet("{id}/{param2=autor}")]
        public async Task<ActionResult<Libro>> GetLibro(int id, string param2)
        {
            var libro = await _context.Libros.FindAsync(id);
            Console.WriteLine(param2);

            if (libro == null)
            {
                return NotFound();
            }

            return libro;
        }

        // GET: api/libros/5?titulo=Libro1&autorId=2**
        /*[HttpGet("{id}")]
        public async Task<ActionResult<Libro>> GetLibroQueryString(int id, string titulo, int autorId)
        {
            var libro = await _context.Libros.FindAsync(id);
            //Console.WriteLine(titulo, autorId);

            if (libro == null)
            {
                return NotFound();
            }

            return libro;
        }*/

        // GET: api/libros/5?titulo
        /*[HttpGet("{id}")]
        public async Task<ActionResult<Libro>> GetLibroParameterRequired(int id, [BindRequired] string titulo)
        {
            var libro = await _context.Libros.FindAsync(id);
            Console.WriteLine(titulo);

            if (libro == null)
            {
                return NotFound();
            }

            return libro;
        }*/

        // PUT: api/libros/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLibro(int id, Libro libro)
        {
            if (id != libro.Id)
            {
                return BadRequest();
            }

            _context.Entry(libro).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LibroExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/libros
        [HttpPost]
        public async Task<ActionResult<Libro>> PostLibro(Libro libro)
        {
            _context.Libros.Add(libro);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLibro", new { id = libro.Id }, libro);
        }

        // DELETE: api/libros/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Libro>> DeleteLibro(int id)
        {
            var libro = await _context.Libros.FindAsync(id);
            if (libro == null)
            {
                return NotFound();
            }

            _context.Libros.Remove(libro);
            await _context.SaveChangesAsync();

            return libro;
        }

        private bool LibroExists(int id)
        {
            return _context.Libros.Any(e => e.Id == id);
        }
    }
}
