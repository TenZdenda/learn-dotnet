using System.ComponentModel.DataAnnotations;

namespace TaskAPI.Services
{
	public class AuthenticateRequest
	{
		[Required]
		public string Email { get; set; }

		[Required]
		public string Password { get; set; }
	}
}
