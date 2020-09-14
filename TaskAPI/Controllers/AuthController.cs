using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskAPI.Models;
using TaskAPI.Repositories;
using TaskAPI.Services;

namespace TaskAPI.Controllers
{
	[Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
	    private IUserRepository _userRepository;

	    public AuthController(IUserRepository userRepository)
	    {
		    _userRepository = userRepository;
	    }

	    [HttpPost]
	    public IActionResult Authenticate(AuthenticateRequest model)
	    {
		    var response = _userRepository.Authenticate(model);

		    if (response == null)
			    return BadRequest(new { message = "Username or password is incorrect" });

		    return Ok(response);
	    }

    }
}
