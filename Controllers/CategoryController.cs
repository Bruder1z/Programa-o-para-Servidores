using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MinhaLoja.Data;
using MinhaLoja.Models;
using MinhaLoja.ViewModels;


namespace MinhaLoja.Controllers
{
    [ApiController]
    public class CategoryController : ControllerBase
    {

        [Authorize(Roles = "Admin")]
        [HttpPost("category")]
        public async Task<IActionResult> PostAsync(
            [FromServices] AppDbContext context,
            [FromBody] CategoryCreateViewModel model)
        {
            try
            {
                var category = new Category
                {
                    Name = model.Name
                };

                await context.Categories.AddAsync(category);
                await context.SaveChangesAsync();

                return Created($"/category/{category.Id}", category);
            }
            catch
            {
                return StatusCode(500, new { message = "Falha interna no servidor" });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("category")]
        public async Task<IActionResult> GetAsync(
            [FromServices] AppDbContext context)
        {
            try
            {
                var categories = await context.Categories.ToListAsync();

                return Ok(categories);
            }
            catch
            {
                return StatusCode(500, new { message = "Falha interna no servidor" });
            }
        }
    }
}
