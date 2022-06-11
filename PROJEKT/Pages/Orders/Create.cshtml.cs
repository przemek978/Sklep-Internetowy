using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using PROJEKT.DAL;
using PROJEKT.Models;
using System;

namespace PROJEKT.Pages.Orders
{
    public class CreateModel : Session
    {
        [BindProperty]
        public Zamowienie newZamowienie { get; set; }
        IZamowienieDB ZamowienieDB;
        public CreateModel(IZamowienieDB _ZamowienieDB)
        {
            ZamowienieDB = _ZamowienieDB;
        }
        public IActionResult OnGet()
        {
            var users = DataBase.ReadUser();
            int uid = 1;
            foreach (var user in users)
            {
                if (user.userName == User.Identity.Name)
                {
                    uid = user.id;
                }
            }
            if (Request.Cookies["Cart"] != null)
            {
                newZamowienie = new Zamowienie(uid, Request.Cookies["Cart"]);
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
