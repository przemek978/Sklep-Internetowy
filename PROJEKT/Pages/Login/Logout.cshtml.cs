using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace PROJEKT.Pages.Login
{

    public class LogoutModel : PageModel
    {
        public async Task<RedirectToPageResult> OnGet()
        {
            await HttpContext.SignOutAsync("CookieAuthentication");
            //return Page();
            return this.RedirectToPage("./Out");
        }
    }


}
