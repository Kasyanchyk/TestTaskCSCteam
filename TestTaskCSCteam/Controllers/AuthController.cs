using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TestTaskCSCteam.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
namespace TestTaskCSCteam.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly IAuthenticationSchemeProvider authenticationSchemeProvider;

        public AuthController(IAuthenticationSchemeProvider authenticationSchemeProvider)
        {
            this.authenticationSchemeProvider = authenticationSchemeProvider;
        }

        /// <summary>
        /// Get authorize status
        /// </summary>
        /// <returns>String with informations</returns>
        [HttpGet]
        public ActionResult<string> Status()
        {
            if(User.Identity.IsAuthenticated)
            {
                return "you are authorized, use api/auth/signout for logout";
            }
            else
            {
                return "you are not authorized, use api/auth/signin for signin";
            }
        }

        /// <summary>
        /// Redirect to Facebook authorize page and redirect to /api/organization
        /// </summary>
        /// <returns>The created Microsoft.AspNetCore.Mvc.ChallengeResult for the response.</returns>
        [HttpGet("signin")]
        public IActionResult SignIn()
        {
            return Challenge(new AuthenticationProperties { RedirectUri = "/api/organization" }, "Facebook");
        }

        /// <summary>
        /// Signout and redirect to /api/organization
        /// </summary>
        /// <returns>Redirect</returns>
        [Authorize]
        [HttpGet("signout")]
        public async Task<IActionResult> SignOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("../../api/organization");
        }
    }
}
