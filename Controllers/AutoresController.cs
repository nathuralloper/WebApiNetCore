using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiNetCore.Contexts;
using WebApiNetCore.Entities;

/*
   FromHeader
   FromQuery
   FromRoute
   FromForm
   FromBody
   FromServices
*/

namespace WebApiNetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutoresController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public AutoresController(ApplicationDbContext context)
        {
            this._context = context;
        }

        [HttpGet("getAll")]
        public ActionResult<IEnumerable<Autor>> Get()
        {
            return _context.Autores.ToList();
        }

        [HttpGet("First")]
        public ActionResult<Autor> GetFirstAutor()
        {
            return _context.Autores.FirstOrDefault();
        }

        //Get Api/autores/5

        [HttpGet("{id}", Name = "GetById")]
        public async Task<ActionResult<Autor>> Get(int id)
        {
            var _autor = await _context.Autores.FirstOrDefaultAsync(x => x.Id == id);
            if (_autor == null)
            {
                return NotFound();
            }

            return _autor;
        }

        [HttpPost]
        public ActionResult Post([FromBody] Autor Value)
        {
            TryValidateModel(Value);
            _context.Autores.Add(Value);
            _context.SaveChanges();
            return new CreatedAtRouteResult("GetById", new { id = Value.Id }, Value);
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Autor Model)
        {
            if (id != Model.Id)
            {
                return BadRequest();
            }

            _context.Entry(Model).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult<Autor> Delete(int id)
        {
            var _autor = _context.Autores.FirstOrDefault(x => x.Id == id);
            if (_autor == null)
            {
                return NotFound();
            }

            _context.Autores.Remove(_autor);
            _context.SaveChanges();
            return _autor;
        }

    }
}
