using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using SecureApp.Services;

namespace SecureApp
{
    public class LoginModel : PageModel
    {
        private readonly LoginByFileService loginProvider;
        private readonly ILogger<LoginModel> logger;

        [BindProperty]
        public string UserName { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public LoginModel(LoginByFileService loginProvider, ILogger<LoginModel> logger)
        {
            this.loginProvider = loginProvider;
            this.logger = logger;
        }

        public async Task<IActionResult> OnPost()
        {
            string user, pass;

            try
            {
                (user, pass) = loginProvider.GetLoginInfo();
            }
            catch (Exception ex)
            {
                logger.LogCritical(ex.Message);
                Response.StatusCode = 500;
                return Content(ex.Message);
            }




            if (String.Compare(UserName, user, false) == 0 && String.Compare(Password, pass, false) == 0)
            {
                //login successful

                var claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.NameIdentifier, UserName));
                claims.Add(new Claim(ClaimTypes.Name, UserName));


                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties()
                {
                    ExpiresUtc = DateTimeOffset.UtcNow.AddDays(30),
                    IsPersistent = true
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

                logger.LogInformation("{0} logged in.", UserName);

                return new RedirectToPageResult("Index");

            }
            else
            {
                //login failed
                TempData.Add("error", "UserName and Password do not match!");
                return Page();

            }
        }
    }
}