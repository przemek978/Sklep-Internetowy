using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using PROJEKT.Models;
using System.Collections.Generic;

namespace PROJEKT.Pages.Users
{
    [Authorize(Roles = "Administrator")]
    public class IndexModel : Session
    {
        public List<SiteUser> Users { get; set; }
        public List<TypeUser> types { get; set; }
        public Product product;
        public int LastID;
        public void OnGet()
        {
            Users = DataBase.ReadUser();
            types= DataBase.ReadTypes();
            SetSession();
        }

    }
}
