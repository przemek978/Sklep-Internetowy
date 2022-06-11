using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PROJEKT.Models;
using System.Threading.Tasks;

namespace PROJEKT.Pages.Login
{

    public class LogoutModel : Session
    {
        public async Task<RedirectToPageResult> OnGet()
        {
            await HttpContext.SignOutAsync("CookieAuthentication");
            //return Page();
            return this.RedirectToPage("./Out");
        }
    }


}
