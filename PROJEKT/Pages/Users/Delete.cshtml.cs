using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using PROJEKT.Models;
using System.Data;
using System.Data.SqlClient;

namespace PROJEKT.Pages.Users
{
    public class DeleteModel : Session
    {
        [Authorize(Roles = "Administrator")]
        public IActionResult OnGet(int id)
        {
            string myCompanyDBcs = DataBase.configuration.GetConnectionString("myCompanyDB");
            SqlConnection con = new SqlConnection(myCompanyDBcs);
            SqlCommand cmd = new SqlCommand("sp_userDel", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter ID_SqlParam = new SqlParameter("@ID", SqlDbType.Int);
            ID_SqlParam.Value = id;
            cmd.Parameters.Add(ID_SqlParam);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            return RedirectToPage("/Users/Index");
        }
    }
}
