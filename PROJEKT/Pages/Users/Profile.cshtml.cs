using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PROJEKT.DAL;
using PROJEKT.Models;
using System.Collections.Generic;

namespace PROJEKT.Pages.Users
{
    [Authorize]
    public class ProfileModel : Session
    {
        public SiteUser user;
        public List<Zamowienie> MyOrders = new List<Zamowienie>();
        public List<Product> Products { get; set; }
        public int[] ile_produktu { get; set; }
        public int licznik = 0;
        IZamowienieDB zamowienieDB;
        public ProfileModel(IZamowienieDB _zamowienieDB)
        {
            zamowienieDB = _zamowienieDB;
        }
        public void OnGet()
        {
            SetSession();
            Products = DataBase.Read();
            user = DataBase.GetUser(User.Identity.Name);
            var Orders = zamowienieDB.List();
            foreach(var o in Orders)
            {
                if(o.UserID==user.id)
                {
                    MyOrders.Add(o);
                }
            }
            
        }
    }
}
