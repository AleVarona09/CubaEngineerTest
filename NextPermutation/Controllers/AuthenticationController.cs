using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NextPermutation.Core;
using NextPermutation.Models;
using System.Security.Claims;

namespace NextPermutation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : Controller
    {
        private readonly IUserRepo _userRepo;
        private readonly AccesTokenGenerator _accesToken;
        private readonly RefreshTokenGenerator _refreshTokenGenerator;
        private readonly RefreshTokenValidator _refreshTokenValidator;
        private readonly IRefreshTokenRepo _refreshTokenRepo;

        public AuthenticationController(IUserRepo userRepo, AccesTokenGenerator accesToken, RefreshTokenGenerator refreshToken, RefreshTokenValidator refreshTokenValidator, IRefreshTokenRepo refreshTokenRepo)
        {
            _userRepo = userRepo;
            _accesToken = accesToken;
            _refreshTokenGenerator = refreshToken;
            _refreshTokenValidator = refreshTokenValidator;
            _refreshTokenRepo = refreshTokenRepo;
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

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            if (!ModelState.IsValid)
            {
                IEnumerable<string> error = ModelState.Values.SelectMany(x => x.Errors.Select(e => e.ErrorMessage));

                return BadRequest(error);
            }

            User user = await _userRepo.GetByUsername(loginRequest.Username);
            if(user == null || user.Password!=loginRequest.Password)
            {
                return Unauthorized();
            }

            string accesToken = _accesToken.GenerateToken(user);
            string refreshToken = _refreshTokenGenerator.GenerateToken();

            RefreshToken refreshDto = new RefreshToken()
            {
                Token = refreshToken,
                UserId = user.Id
            };
            await _refreshTokenRepo.Create(refreshDto);

            return Ok(new ResponseAuth()
            {
                AccesToken = accesToken, 
                RefreshToken = refreshToken
            }); 
        }

        [Authorize]
        [HttpDelete("logout")]
        public async Task<IActionResult> Logout()
        {
            string id = HttpContext.User.FindFirstValue("id");
            if(!Guid.TryParse(id,out Guid userId))
            {
                return Unauthorized();
            }

            await _refreshTokenRepo.DeleteByUserId(userId);

            return NoContent();

        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshRequest refreshRequest)
        {
            if (!ModelState.IsValid)
            {
                IEnumerable<string> error = ModelState.Values.SelectMany(x => x.Errors.Select(e => e.ErrorMessage));

                return BadRequest(error);
            }

            bool validation = _refreshTokenValidator.ValidateToken(refreshRequest.RefreshToken);

            if (!validation)
            {
                return BadRequest("Invalid refresh token.");
            }

            RefreshToken token = await _refreshTokenRepo.GetByToken(refreshRequest.RefreshToken);
            if (token == null)
            {
                return NotFound("No token was found.");
            }

            User user = await _userRepo.GetById(token.UserId);
            if (user == null)
            {
                return NotFound("No user was found.");
            }

            await _refreshTokenRepo.Delete(token);

            string accesToken = _accesToken.GenerateToken(user);
            string refreshToken = _refreshTokenGenerator.GenerateToken();

            RefreshToken refreshDto = new RefreshToken()
            {
                Token = refreshToken,
                UserId = user.Id
            };
            await _refreshTokenRepo.Create(refreshDto);

            return Ok(new ResponseAuth()
            {
                AccesToken = accesToken,
                RefreshToken = refreshToken
            });
        }

    }
}
