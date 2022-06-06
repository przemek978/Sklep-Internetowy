using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using PROJEKT.DAL;
using PROJEKT.Models;

namespace PROJEKT.Pages
{
    public class CreateModel : PageModel
    {
        [BindProperty]
        public Zamowienie newZamowienie { get; set; }
        private readonly IConfiguration _configuration;
        IZamowienieDB ZamowienieDB;
        public CreateModel(IZamowienieDB _ZamowienieDB, IConfiguration configuration)
        {
            ZamowienieDB = _ZamowienieDB;
            _configuration = configuration;
        }
        public IActionResult OnGet()
        {
            var users = DataBase.ReadUser(_configuration);
            int id = 1;
            foreach (var user in users)
            {
                if(user.userName ==User.Identity.Name)
                {
                    id = user.id;
                }
            }
            if (Request.Cookies["Cart"] != null)
            {
                newZamowienie = new Zamowienie(id, Request.Cookies["Cart"]);
                ZamowienieDB.Add(newZamowienie);
            }
            var cookieOptions = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(-1)
            };
            Response.Cookies.Append("Cart", "", cookieOptions);
            return RedirectToPage("/Cart/Cart");
        }

    }
}