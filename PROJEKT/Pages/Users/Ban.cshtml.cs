using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using PROJEKT.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace PROJEKT.Pages.Users
{
    public class BanModel : Session
    {
        [BindProperty]
        public SiteUser user { get; set; }
        public List<TypeUser> types { get; set; }
        public BanModel(IConfiguration configuration)
        {
            DataBase.configuration = configuration;
        }
        public IActionResult OnGet(int id)
        {
            user = DataBase.GetUser(id);
            if (!ModelState.IsValid)
            {
                return Page();
            }
            bool Active;
            if(user.isActive==true)
            {
                Active = false; 
            }
            else Active= true;
            string myCompanyDBcs = DataBase.configuration.GetConnectionString("myCompanyDB");
            SqlConnection con = new SqlConnection(myCompanyDBcs);
            SqlCommand cmd = new SqlCommand("sp_userBan", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter ID_SqlParam = new SqlParameter("@ID", SqlDbType.Int);
            ID_SqlParam.Value = id;
            cmd.Parameters.Add(ID_SqlParam);
            SqlParameter active_SqlParam = new SqlParameter("@active", SqlDbType.Bit);
            active_SqlParam.Value = Active;
            cmd.Parameters.Add(active_SqlParam);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            return RedirectToPage("/Users/Index");
        }
    }
}
