using System.ComponentModel.DataAnnotations;

namespace TaskAPI.Models
{
	public class Category
	{
		public int Id { get; set; }

		[Required]
		public string Name { get; set; }
	}
}
