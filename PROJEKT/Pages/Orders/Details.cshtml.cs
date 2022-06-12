using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PROJEKT.DAL;
using PROJEKT.Models;

namespace PROJEKT.Pages.Orders
{
    [Authorize(Roles = "Administrator,Kierownik,Pracownik")]
    public class DetailsModel : Session
    {
        public Zamowienie Z { get; set; }
        IZamowienieDB ZamowienieDB;
        public SiteUser user { get; set; }
        public List<Product> products { get; set; }
        public List<SiteUser> users { get; set; }
        public int[] ile_produktu { get; set; }
        public DetailsModel(IZamowienieDB _ZamowienieDB)
        {
            ZamowienieDB = _ZamowienieDB;
        }
        public void OnGet(int id)
        {
            user = new SiteUser();
            Z = ZamowienieDB.Get(id);
            users = DataBase.ReadUser();
            products = DataBase.Read();
            ile_produktu = new int[products.Count + 1];
            foreach (var u in users)
            {
                if (u.id == Z.UserID)
                {
                    user.userName = u.userName;
                }
            }
            for (int i = 0; i < Z.produkty.Length; i++)
            {
                ile_produktu[Z.produkty[i] - 48]++;
            }
        }
        public IActionResult OnPost()
        {
            return RedirectToPage("Cart/Cart");
        }
    }
}