using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PROJEKT.DAL;
using PROJEKT.Models;

namespace PROJEKT.Pages.Orders
{
    [Authorize(Roles = "Administrator,Kierownik,Pracownik")]
    public class DeleteModel : Session
    {
        IZamowienieDB ZamowienieDB;
        public DeleteModel(IZamowienieDB _ZamowienieDB)
        {
            ZamowienieDB = _ZamowienieDB;
        }
        public IActionResult OnGet(int id)
        {
            ZamowienieDB.Delete(id);
            return RedirectToPage("/Orders/Index");
        }
    }
}