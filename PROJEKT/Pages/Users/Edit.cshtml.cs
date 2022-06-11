using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using PROJEKT.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace PROJEKT.Pages.Users
{
    public class EditModel : Session
    {
        public string Message { get; set; }
        [BindProperty]
        public SiteUser user { get; set; }
        public List<TypeUser> types { get; set; }
        public EditModel(IConfiguration configuration)
        {
            DataBase.configuration = configuration;
        }
        public void OnGet(int id)
        {
            user = DataBase.GetUser(id);
        }
        public IActionResult OnPost(int id)
        {
            if (!ModelState.IsValid)
            {
                user = DataBase.GetUser(id);
                return Page();
            }
            string myCompanyDBcs = DataBase.configuration.GetConnectionString("myCompanyDB");
            SqlConnection con = new SqlConnection(myCompanyDBcs);
            SqlCommand cmd = new SqlCommand("sp_userEdit", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter ID_SqlParam = new SqlParameter("@ID", SqlDbType.Int);
            ID_SqlParam.Value = id;
            cmd.Parameters.Add(ID_SqlParam);
            SqlParameter userName_SqlParam = new SqlParameter("@userName", SqlDbType.Char, 64);
            userName_SqlParam.Value = user.userName;
            cmd.Parameters.Add(userName_SqlParam);
            SqlParameter email_SqlParam = new SqlParameter("@email", SqlDbType.Char, 64);
            email_SqlParam.Value = user.email;
            cmd.Parameters.Add(email_SqlParam);
            SqlParameter active_SqlParam = new SqlParameter("@active", SqlDbType.Bit);
            active_SqlParam.Value = user.isActive;
            cmd.Parameters.Add(active_SqlParam);
            SqlParameter type_SqlParam = new SqlParameter("@typeID", SqlDbType.Int, 64);
            type_SqlParam.Value = user.typeID;
            cmd.Parameters.Add(type_SqlParam);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            return RedirectToPage("/Users/Index");
        }
    }
}
