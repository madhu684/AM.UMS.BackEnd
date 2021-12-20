using AM.UMS.BackEnd.Data;
using AM.UMS.BackEnd.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AM.UMS.BackEnd.DAL
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly ILogger<AuthenticationService> _logger;
        private readonly UserDbContext _context;
        private readonly AppSettings _appSettings;

        public AuthenticationService()
        {
        }

        public AuthenticationService(ILogger<AuthenticationService> logger, UserDbContext context, IOptions<AppSettings> appSettings)
        {
            _logger = logger;
            _context = context;
            _appSettings = appSettings.Value;
        }
        public User Authenticate(string username, string password)
        {
            try
            {
                _logger?.LogInformation("Querying the User...");
                var user = _context.Users.Where(x => x.Username == username && x.Password == password).FirstOrDefault();

                //return null if user is not found
                if (user == null)
                {
                    _logger?.LogInformation("User not found");
                    return null;
                }

                //if user found
                _logger?.LogInformation("Generating the token...");
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSettings.Key);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.Name, user.Username.ToString()),
                    new Claim(ClaimTypes.Role, "Admin"),
                    new Claim(ClaimTypes.Version, "V3.1"),

                    }),
                    Expires = DateTime.UtcNow.AddDays(2),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                user.Token = tokenHandler.WriteToken(token);

                user.Password = null;
                return user;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                throw ex;
            }
        }
    }
}
