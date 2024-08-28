using Agenda.Data;
using Agenda.Models;
using Microsoft.AspNetCore.Mvc;


namespace Agenda.Controllers
{
    [ApiController]

    public class HomeController : ControllerBase
    {
        // obter todos contatos
        [HttpGet("/")]
        public IActionResult Get([FromServices]AppDbContext context)//recber o contexto dentro do ()
        {
            var todosContatos = context.Contatos.ToList();
            return Ok(todosContatos);
        }
        [HttpPost("/")]

        public IActionResult Post(
            [FromBody]Contato umContato,
            [FromServices]AppDbContext context)
        {
            context.Contatos.Add(umContato);
            context.SaveChanges();

            return Created($"/{umContato.Id}", umContato);
        }

    // para obter um contato por id
    [HttpGet("/{id:int}")]
    public IActionResult GetById(
        [FromRoute] int id,
        [FromServices]AppDbContext context)
        {
            var umContato = context.Contatos.Find(id);

            if(umContato is null)
                return NotFound();

            return Ok(umContato);
        }

    }
}