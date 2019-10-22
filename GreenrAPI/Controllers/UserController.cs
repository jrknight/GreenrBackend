using GreenrAPI.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GreenrAPI.Controllers
{
    [Route("api/user")]
    public class UserController : Controller
    {

        private UserManager<User> userMgr;
        private IHttpContextAccessor httpContextAccessor;

        public UserController(UserManager<User> userManager, IHttpContextAccessor accessor)
        {
            userMgr = userManager;
            httpContextAccessor = accessor;
        }

        [HttpGet("current")]
        public async Task<IActionResult> GetLoggedInUser()
        {
            var userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            
            return Ok(await userMgr.FindByIdAsync(userId).ConfigureAwait(false));
        }

    }
}
