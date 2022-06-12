using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PROJEKT.Models;

namespace PROJEKT.Pages
{
    public class WelcomeModel : Session
    {
        public IActionResult OnGet()
        {
            if (Request.Cookies["UserLoginCookie"] != null)
            {
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}
