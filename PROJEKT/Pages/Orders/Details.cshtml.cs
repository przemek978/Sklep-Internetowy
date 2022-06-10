using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PROJEKT.DAL;
using PROJEKT.Models;

namespace PROJEKT.Pages
{
    public class DetailsModel : PageModel
    {
        public Zamowienie Zamowienie { get; set; }
        IZamowienieDB ZamowienieDB;
        public DetailsModel(IZamowienieDB _ZamowienieDB)
        {
            ZamowienieDB = _ZamowienieDB;
        }
        public void OnGet(int id)
        {
            Zamowienie = new Zamowienie();
            Zamowienie = ZamowienieDB.Get(id);
        }
        public IActionResult OnPost()
        {
            return RedirectToPage("Cart/Cart");
        }
    }
}