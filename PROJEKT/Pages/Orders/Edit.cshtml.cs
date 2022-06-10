using System;
using System.Xml;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PROJEKT.DAL;
using PROJEKT.Models;

namespace PROJEKT.Pages.Orders
{
    //[Authorize(Roles = "Administrator,Kierownik,Pracownik")]
    public class EditModel : PageModel
    {
        [BindProperty]
        public Zamowienie Zamowienie { get; set; }
        IZamowienieDB ZamowienieDB;
        public EditModel(IZamowienieDB _ZamowienieDB)
        {
            ZamowienieDB = _ZamowienieDB;
        }
        public void OnGet(int id)
        {
            Zamowienie = ZamowienieDB.Get(id);
        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            ZamowienieDB.Update(Zamowienie);
            return RedirectToPage("/Orders/Index");
        }
    }
}