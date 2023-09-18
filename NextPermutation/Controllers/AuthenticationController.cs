using Microsoft.AspNetCore.Mvc;
using NextPermutation.Core;
using NextPermutation.Models;

namespace NextPermutation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : Controller
    {
        private readonly IUserRepo _userRepo;


        public AuthenticationController(IUserRepo userRepo)
        {
            _userRepo = userRepo;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest)
        {
            if (!ModelState.IsValid)
            {
                IEnumerable<string> error = ModelState.Values.SelectMany(x => x.Errors.Select(e => e.ErrorMessage));

                return BadRequest(error);
            }

            if (registerRequest.Password != registerRequest.ConfirmPassword) 
            {
                return BadRequest("Password don't match.");
            }

            User userEmail = await _userRepo.GetByEmail(registerRequest.Email);
            if (userEmail != null)
            {
                return Conflict("Email in use.");
            }

            User userName = await _userRepo.GetByUsername(registerRequest.Username);
            if (userName != null)
            {
                return Conflict("Username in use.");
            }

            User userReg = new User()
            {
                Email = registerRequest.Email,
                Username = registerRequest.Username,
                Password = registerRequest.Password,
            };

            return Ok(await _userRepo.CreateUser(userReg));             
        }
    
    
    }
}
