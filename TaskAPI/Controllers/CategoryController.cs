using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskAPI.Models;

namespace TaskAPI.Controllers
{
	[Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
	    private readonly TaskAPIContext _context;

	    public CategoryController(TaskAPIContext context)
	    {
		    _context = context;
	    }

	    [HttpGet]
	    public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
	    {
		    return await _context.Categories.ToListAsync();
	    }

	    // GET: api/Category/5
	    [Authorize]
	    [HttpGet("{id}")]
	    public async Task<ActionResult<Category>> GetCategory(int id)
	    {
		    var category = await _context.Categories.FindAsync(id);

		    if (category == null)
		    {
			    return NotFound();
		    }

		    return category;
	    }

	    // POST: api/Category
	    [HttpPost]
	    public async Task<ActionResult<Category>> CreateCategory(Category category)
	    {
		    _context.Categories.Add(category);
		    await _context.SaveChangesAsync();

		    return CreatedAtAction("GetCategory", new { id = category.Id }, category);
	    }

	    // DELETE: api/Category/5
	    [HttpDelete("{id}")]
	    public async Task<ActionResult<Category>> DeleteCategory(int id)
	    {
		    var category = await _context.Categories.FindAsync(id);
		    if (category == null)
		    {
			    return NotFound();
		    }

		    _context.Categories.Remove(category);
		    await _context.SaveChangesAsync();

		    return Ok("Category was deleted");
	    }
    }
}
