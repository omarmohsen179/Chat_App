using Chat_App.Core;
using Chat_App.Helper;
using Chat_App.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Http;

using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;

using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using System.IO;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
namespace Chat_App.Controllers
{
    public class Response {
        public string Status { get; set; }
        public string Message { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IWebHostEnvironment _enviroment;
        public static ImageUploder _imageUploder;

        private readonly IUnitOfWork unitOfWork;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private IConfiguration _configuration;
        private readonly SignInManager<ApplicationUser> signInManager;
        public UserController( IUnitOfWork unitOfWork, IWebHostEnvironment enviroment, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration _configuration, RoleManager<IdentityRole> roleManager)
        {

            this.unitOfWork = unitOfWork;
            this.signInManager = signInManager;
            this.userManager = userManager;
            this._enviroment = enviroment;
            _imageUploder = new ImageUploder(enviroment);
            this._configuration = _configuration;
            this.roleManager = roleManager;
        }
        [HttpPost("LogIn")]
        public async Task<IActionResult> LogInUserAsync([FromBody] LoginForm model)
        {
            var user = await userManager.FindByNameAsync(model.username);
            if (user != null && await userManager.CheckPasswordAsync(user, model.password))
            {
                var userRoles = await userManager.GetRolesAsync(user);
                if (userRoles.Count == 0 && !user.EmailConfirmed)
                {
                    return Ok(110);

                }
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

                var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddHours(3),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo,
                    username = model.username,
                });


            }
            return Unauthorized();

        }
        [HttpPost]
        [Route("register-user")]
        public async Task<IActionResult> Register([FromBody] RegistrationForm model)
        {

            var username = await userManager.FindByNameAsync(model.username);
            if (username != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });
            var userEmail = await userManager.FindByEmailAsync(model.email);
            if (userEmail != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });
            ApplicationUser user = new ApplicationUser()
            {
                Email = model.email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.username,
            };
            var result = await userManager.CreateAsync(user, model.password);
            var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
            var local = "http://mmm.almedadsoft.com/email-confirmation";

            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });

            return Ok(new Response { Status = "Success", Message = "User created successfully!" });
        }
    }
}
