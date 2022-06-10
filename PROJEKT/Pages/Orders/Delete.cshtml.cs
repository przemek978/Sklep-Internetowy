using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PROJEKT.DAL;

namespace PROJEKT.Pages
{
    [Authorize(Roles = "Administrator,Kierownik,Pracownik")]
    public class DeleteModel : PageModel
    {
        IZamowienieDB ZamowienieDB;
        public DeleteModel(IZamowienieDB _ZamowienieDB)
        {
            ZamowienieDB = _ZamowienieDB;
        }
        public IActionResult OnGet(int id)
        {
            ZamowienieDB.Delete(id);
            return RedirectToPage("/Cart/Cart");
        }
    }
}