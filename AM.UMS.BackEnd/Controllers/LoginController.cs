using AM.UMS.BackEnd.Data;
using AM.UMS.BackEnd.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AM.UMS.BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IAuthenticationService _authenticationService;
    
        public LoginController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost]
        public IActionResult Post([FromBody] User model)
        {
            var user = _authenticationService.Authenticate(model.Username, model.Password);

            if(user == null)
            {
                return BadRequest(new { message = "Username or password is invalid" });
            }
            else
            {
                return Ok(user);
            }
        }
    }
}
