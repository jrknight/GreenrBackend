using GreenrAPI.Entities;
using GreenrAPI.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GreenrAPI.Controllers
{
    [Route("api/auth")]
    public class AuthController : Controller
    {
        
        private SignInManager<User> signInMgr;
        private UserManager<User> userMgr;
        private IPasswordHasher<User> passwordHasher;
        private IConfigurationRoot config;

        public AuthController(SignInManager<User> signInManager,
            UserManager<User> userManager,
            IPasswordHasher<User> hasher,
            IConfiguration config,
            IHostingEnvironment environment)
        {
            signInMgr = signInManager;
            userMgr = userManager;
            passwordHasher = hasher;

            var builder = new ConfigurationBuilder().SetBasePath(environment.ContentRootPath).AddJsonFile("config.json");

            config = builder.Build();
        }


        [ValidateModel]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] CredentialModel model)
        {
            try
            {
                var result = await signInMgr.PasswordSignInAsync(model.UserName, model.Password, false, false).ConfigureAwait(false);
                if (result.Succeeded)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception on Authentication" + ex);
                return BadRequest("There was a critical problem logging in.");
            }
        }


        [ValidateModel]
        [HttpPost("token")]
        public async Task<IActionResult> CreateToken([FromBody] CredentialModel model)
        {
            try
            {
                var user = await userMgr.FindByNameAsync(model.UserName).ConfigureAwait(false);
                if (user != null)
                {
                    if ((await signInMgr.CheckPasswordSignInAsync(user, model.Password, false).ConfigureAwait(false)).Succeeded)
                    {
                        var claims = new[] {
                            new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                        };
                        var guid = config["Auth:GUID"];
                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(guid));
                        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                        var token = new JwtSecurityToken(
                            issuer: config["Auth:Token:Issuer"],
                            audience: config["Auth:Token:Audience"],
                            claims: claims,
                            expires: DateTime.UtcNow.AddMonths(1),
                            signingCredentials: creds
                            );



                        return Json(new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(token),
                            expiration = token.ValidTo,
                            role = userMgr.GetRolesAsync(user).Result,
                            currentUser = user
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception thrown while creating a JWT: {ex}");
                return BadRequest("There was a problem creating the JWT");
            }

            return BadRequest("Failed to generate token.");
        }


        [HttpPost("newuser")]
        public async Task<IActionResult> NewUser([FromBody] CredentialModel model)
        {

            if (model == null)
            {
                return BadRequest($"The request body can't be null {model}");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await userMgr.FindByNameAsync(model.UserName).ConfigureAwait(false);

            if (user == null)
            {
                var newUser = new User()
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    FullName = model.Name

                };

                var result = await userMgr.CreateAsync(newUser, model.Password).ConfigureAwait(false);

                if (result.Succeeded)
                {

                    //newUser = ctx.Users.Where(u => u.UserName == newUser.UserName).FirstOrDefault();


                    return Created($"api/auth/login", result);
                }
                else
                {
                    await userMgr.DeleteAsync(newUser).ConfigureAwait(false);
                    return BadRequest(result);
                }
            }

            ///TODO: Add extra checks for correct provided information  

            return BadRequest($"A user already exists with the username \"{model.UserName}\".");
        }
    }
}
