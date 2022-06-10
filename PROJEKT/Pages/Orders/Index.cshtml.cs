using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PROJEKT.DAL;
using PROJEKT.Models;
using System.Collections.Generic;
using System.Xml;

namespace PROJEKT.Pages.Orders
{
    public class IndexModel : PageModel
    {
        public List<Zamowienie> Zamowienia;
        IZamowienieDB zamowienieDB;
        public IConfiguration _configuration { get; }
        SiteUser user { get; set; }
        public List<Product> products { get; set; }
        public List<SiteUser> users { get; set; }
        public int[] ile_produktu { get; set; }
        public IndexModel(IZamowienieDB _zamowienieDB, IConfiguration configuration)
        {
            zamowienieDB= _zamowienieDB;    
           _configuration = configuration;
        }
        public void OnGet()
        {
            Zamowienia = zamowienieDB.List();
            users = DataBase.ReadUser(_configuration);
            products = DataBase.Read(_configuration);
            ile_produktu = new int[products.Count + 1];
        }
        private Zamowienie XmlNode2Product(XmlNode node)
        {
            Zamowienie z = new Zamowienie();
            z.id = int.Parse(node.Attributes["id"].Value);
            z.UserID = int.Parse(node.Attributes["UserID"].Value);
            z.produkty = node["produkty"].InnerText;
            return z;
        }
    }
}
