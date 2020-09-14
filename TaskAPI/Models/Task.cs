using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskAPI.Models
{
	public class Task
	{
		public int Id { get; set; }

		[Required]
		public string Title { get; set; }

		public Category Category { get; set; }

		[DefaultValue(false)]
		public bool Finished { get; set; }
	}
}
