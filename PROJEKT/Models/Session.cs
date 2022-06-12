using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PROJEKT.Models
{
    public class Session: PageModel
    {
        public void SetSession()
        {
            HttpContext.Session.SetString("PreviousLink", PageContext.ActionDescriptor.DisplayName);
        }
        public void SetSession(string url)
        {
            HttpContext.Session.SetString("PreviousLink", url);
        }
        public string GetSession()
        {
            return HttpContext.Session.GetString("PreviousLink");
        }
    }
}
