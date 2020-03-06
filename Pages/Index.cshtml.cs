using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SecureApp
{
    public class IndexModel : PageModel
    {
        public async Task<IActionResult> OnPostLogout()
        {
            await HttpContext.SignOutAsync();

            return RedirectToPage("Index");
        }
    }
}