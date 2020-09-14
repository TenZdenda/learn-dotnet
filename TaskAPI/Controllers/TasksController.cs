using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskAPI.Models;
using Task = TaskAPI.Models.Task;

namespace TaskAPI.Controllers
{
	[Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
	    private readonly TaskAPIContext _context;

	    public TasksController(TaskAPIContext context)
	    {
		    _context = context;
	    }

	    // GET: api/Tasks
	    [HttpGet]
	    public async Task<ActionResult<IEnumerable<Task>>> GetTasks()
	    {
		    return await _context.Task.Include(b => b.Category).ToListAsync();
	    }

	    // GET: api/Tasks/5
	    [HttpGet("{id}")]
	    public async Task<ActionResult<Task>> GetTask(int id)
	    {
		    var task = await _context.Task.FindAsync(id);

		    if (task == null)
		    {
			    return NotFound();
		    }

		    return task;
	    }

	    // POST: api/Tasks
	    [HttpPost]
	    public async Task<ActionResult<Task>> CreateTask(Task task)
	    {
		    _context.Task.Add(task);
		    await _context.SaveChangesAsync();

		    return CreatedAtAction("GetTask", new { id = task.Id }, task);
	    }

	    // DELETE: api/Tasks/5
	    [HttpDelete("{id}")]
	    public async Task<ActionResult<Task>> DeleteTask(int id)
	    {
		    var task = await _context.Task.FindAsync(id);
		    if (task == null)
		    {
			    return NotFound();
		    }

		    _context.Task.Remove(task);
		    await _context.SaveChangesAsync();

		    return task;
	    }
    }
}
