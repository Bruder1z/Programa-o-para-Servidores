using Biblioteca.Data;
using Biblioteca.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace Biblioteca.Controllers
{
    [ApiController]
    [Route("livros")]
    public class LivrosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public LivrosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var todosLivros = _context.Livrarias.ToList();
            return Ok(todosLivros);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Livraria umLivro)
        {
            if (umLivro.DataPublicacao != default)
            {
                umLivro.DataPublicacao = DateTime.Parse(umLivro.DataPublicacao.ToString("yyyy-MM-dd"));
            }
            else
            {
                return BadRequest("Data de publicação inválida.");
            }

            _context.Livrarias.Add(umLivro);
            _context.SaveChanges();

            return Created($"/livros/{umLivro.Id}", umLivro);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var umLivro = _context.Livrarias.Find(id);

            if (umLivro == null)
                return NotFound();

            return Ok(umLivro);
        }

        [HttpPut("{id:int}")]
        public IActionResult Put([FromRoute] int id, [FromBody] Livraria livroAtualizado)
        {
            var livro = _context.Livrarias.Find(id);
            if (livro == null)
                return NotFound();

            livro.Titulo = livroAtualizado.Titulo;
            livro.Autor = livroAtualizado.Autor;
            livro.Editora = livroAtualizado.Editora;
            livro.Estoque = livroAtualizado.Estoque;
            livro.DataPublicacao = livroAtualizado.DataPublicacao;

            _context.Livrarias.Update(livro);
            _context.SaveChanges();

            return Ok("Livro atualizado com sucesso.");
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var livro = _context.Livrarias.Find(id);
            if (livro == null)
                return NotFound();

            _context.Livrarias.Remove(livro);
            _context.SaveChanges();

            return Ok("Livro excluído com sucesso.");
        }

        [HttpPost("{id:int}/adicionar-estoque")]
        public IActionResult AdicionarEstoque([FromRoute] int id, [FromBody] int quantidade)
        {
            var livro = _context.Livrarias.Find(id);
            if (livro == null)
                return NotFound();

            livro.Estoque += quantidade;
            _context.Livrarias.Update(livro);
            _context.SaveChanges();

            return Ok("Estoque atualizado com sucesso.");
        }

        [HttpPost("{id:int}/remover-estoque")]
        public IActionResult RemoverEstoque([FromRoute] int id, [FromBody] int quantidade)
        {
            var livro = _context.Livrarias.Find(id);
            if (livro == null)
                return NotFound();

            if (livro.Estoque < quantidade)
                return BadRequest("Quantidade de estoque insuficiente.");

            livro.Estoque -= quantidade;
            _context.Livrarias.Update(livro);
            _context.SaveChanges();

            return Ok("Estoque removido com sucesso.");
        }

        [HttpGet("editora/{editora}")]
        public IActionResult GetPorEditora([FromRoute] string editora)
        {
            var livrosPorEditora = _context.Livrarias.Where(l => l.Editora == editora).ToList();
            return Ok(livrosPorEditora);
        }
    }
}
