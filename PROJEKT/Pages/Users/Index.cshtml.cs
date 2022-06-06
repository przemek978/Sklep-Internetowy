using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using PROJEKT.Models;
using System.Collections.Generic;

namespace PROJEKT.Pages.Users
{
    public class IndexModel : PageModel
    {
        //////////////////////////////////////////////////////////////////////////////////////////
        public IConfiguration _configuration { get; }
        public IndexModel(IConfiguration configuration)
        {
            _configuration = configuration;

        }
        //////////////////////////////////////////////////////////////////////////////////////////
        public List<SiteUser> Users { get; set; }
        public List<TypeUser> types { get; set; }
        public Product product;
        public int LastID;
        public void OnGet()
        {
            Users = DataBase.ReadUser(_configuration);
            types= DataBase.ReadTypes(_configuration);
        }

    }
}
