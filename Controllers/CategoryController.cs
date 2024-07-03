using Blog.Data;
using Blog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blog.Controllers
{
    [ApiController]
    public class CategoryController : ControllerBase
    {
        [HttpGet("v1/categories")]
        public async Task<IActionResult> GetAsync ( [FromServices] BlogDataContext context)
        {
            try
            {
                var categories = await context.Categories.ToListAsync();
                return Ok(categories);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "06XEX - Falha interna no servidor");
            }
        }

        [HttpGet("v1/categories/{id:int}")]
        public async Task<IActionResult> GetByIdAsync ( [FromServices] BlogDataContext context, [FromRoute] int id)
        {
            try
            {
                var category = await context.Categories.FirstOrDefaultAsync(c => c.Id == id);
                if (category == null)
                    return NotFound();

                return Ok(category);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "07XEX - Falha interna no servidor");
            }

        }

        [HttpPost("v1/categories")]
        public async Task<IActionResult> PostAsync ( [FromServices] BlogDataContext context, [FromBody] Category model)
        {
            try
            {
                var category = await context.Categories.AddAsync(model);
                await context.SaveChangesAsync();

                return Ok(category);
            }
            catch(DbUpdateException ex)
            {
                return StatusCode(500, "08XEX - Não foi possível incluir a categoria");
            }
            
            catch (Exception ex)
            {
                return StatusCode(500, "09XEX - Falha interna no servidor");
            }
        }

        [HttpPut("v1/categories/{id:int}")]
        public async Task<IActionResult> PutAsync ( [FromServices] BlogDataContext context, [FromBody] Category model, [FromRoute] int id)
        {
            try
            {
                var category = await context.Categories.FirstOrDefaultAsync(c => c.Id == id);
                if (category == null)
                    return NotFound();

                category.Name = model.Name;
                category.Slug = model.Slug;

                context.Categories.Update(category);
                await context.SaveChangesAsync();

                return Ok(category);
            }
            catch(DbUpdateException ex)
            {
                return StatusCode(500, "010XEX - Não foi possivel alterar a categoria");
            }
            catch(Exception ex)
            {
                return StatusCode(500, "011XEX - Falha interna no servidor");
            }
            
        }

        [HttpDelete("v1/categories/{id:int}")]
        public async Task<IActionResult> DeleteAsync ( [FromServices]BlogDataContext context, [FromRoute] int id)
        {
            try
            {
                var category = await context.Categories.FirstOrDefaultAsync(c => c.Id == id);
                if (category == null)
                    return NotFound();

                context.Categories.Remove(category);
                await context.SaveChangesAsync();

                return Ok(category);
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, "012XEX - Falha interna no servidor");
            }
        }
    }
}
