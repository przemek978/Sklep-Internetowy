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
            if(GetSession()== "/Orders/Index" || GetSession()== "/Users/Index")
            {
                SetSession("/Products/Index");
            }
            return this.RedirectToPage("./Out");
        }
    }


}
