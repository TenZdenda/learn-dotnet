using Microsoft.EntityFrameworkCore;

namespace TaskAPI.Models
{
	public class TaskAPIContext : DbContext
	{
		public TaskAPIContext (DbContextOptions<TaskAPIContext> options) : base(options)
		{

		}

		public DbSet<Task> Task { get; set; }
		public DbSet<Category> Categories { get; set; }
	}
}
