using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using TaskAPI.Helpers;
using TaskAPI.Models;
using TaskAPI.Services;

namespace TaskAPI.Repositories
{
	public interface IUserRepository
	{
		AuthenticateResponse Authenticate(AuthenticateRequest model);
		IEnumerable<User> GetAll();
		User GetById(int id);
	}

	public class UserRepository : IUserRepository
	{
		private List<User> _users = new List<User>
		{
			new User { Id = 1, Email = "test@test.cz", Password = "test" }
		};

		private readonly AppSettings _appSettings;

		public UserRepository(IOptions<AppSettings> appSettings)
		{
			_appSettings = appSettings.Value;
		}

		public AuthenticateResponse Authenticate(AuthenticateRequest model)
		{
			var user = _users.SingleOrDefault(x => x.Email == model.Email && x.Password == model.Password);

			// return null if user not found
			if (user == null) return null;

			// authentication successful so generate jwt token
			var token = generateJwtToken(user);

			return new AuthenticateResponse(user, token);
		}

		public IEnumerable<User> GetAll()
		{
			return _users;
		}

		public User GetById(int id)
		{
			return _users.FirstOrDefault(x => x.Id == id);
		}

		// helper methods

		private string generateJwtToken(User user)
		{
			// generate token that is valid for 7 days
			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
				Expires = DateTime.UtcNow.AddDays(7),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
			};
			var token = tokenHandler.CreateToken(tokenDescriptor);
			return tokenHandler.WriteToken(token);
		}
	}
}
