using MarCorp.DemoBack.Application.DTO;
using MarCorp.DemoBack.Application.Interface.UseCases;
using MarCorp.DemoBack.Services.WebApi.Helpers;
using MarCorp.DemoBack.Support.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MarCorp.DemoBack.Services.WebApi.Controllers
{
    /// <summary>
    /// Controller for managing user-related actions.
    /// </summary>
    [Authorize]
    [EnableRateLimiting("fixedWindow")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersApplication _usersApplication;
        private readonly AppSettings _appSettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="UsersController"/> class.
        /// </summary>
        /// <param name="authApplication">The user application service.</param>
        /// <param name="appSettings">The application settings.</param>
        public UsersController(IUsersApplication authApplication, IOptions<AppSettings> appSettings)
            => (_usersApplication, _appSettings) = (authApplication, appSettings.Value);

        /// <summary>
        /// Authenticates a user and returns a token if successful.
        /// </summary>
        /// <param name="usersDTO">The user data transfer object containing login information.</param>
        /// <returns>An IActionResult containing the authentication result.</returns>
        [AllowAnonymous]
        [HttpPost("AuthenticateAsync")]
        public async Task<IActionResult> AuthenticateAsync([FromBody] UserDTO usersDTO)
        {
            var response = await _usersApplication.AuthenticateAsync(usersDTO.UserName, usersDTO.Password);
            if (response.IsSuccess)
            {
                if (response.Data != null)
                {
                    response.Data.Token = BuildToken(response);
                    return Ok(response);
                }
                else
                    return NotFound(response);
            }

            return BadRequest(response);
        }

        /// <summary>
        /// Builds a token for the user.
        /// </summary>
        /// <param name="usersDTO"></param>
        /// <returns></returns>
        private string BuildToken(Response<UserDTO> usersDTO)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret.PadRight(32));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] { new Claim(ClaimTypes.Name, usersDTO.Data.UserId.ToString()) }),
                Expires = DateTime.UtcNow.AddMinutes(100),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _appSettings.Issuer,
                Audience = _appSettings.Audience
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            return tokenString;
        }
    }
}
