using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using PROJEKT.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;

namespace PROJEKT.Pages.Login
{
    [Authorize]
    public class ChangePassModel : Session
    {
        public string Message { get; set; }
        [BindProperty]
        public SiteUser user { get; set; }
        public SiteUser user1 { get; set; }
        public List<TypeUser> types { get; set; }
        [DisplayName("Powtórz has³o")]
        [DataType(DataType.Password)]
        public string password { get; set; }
        public IActionResult OnGet(int id)
        {
            user = DataBase.GetUser(id);
            user1 = DataBase.GetUser(User.Identity.Name);
            if (user.id!=user1.id && user1.typeID!=1)
            {
                return RedirectToPage("/Account/AccessDenied");
            }
            return Page();
        }
        public IActionResult OnPost(int id, string password)
        {
            var u = DataBase.GetUser(id);
            if (!ModelState.IsValid)
            {
                user = DataBase.GetUser(id);
                return Page();
            }
            if (user.password==password)
            {
                user.isActive = true;
                string myCompanyDBcs = DataBase.configuration.GetConnectionString("myCompanyDB");
                SqlConnection con = new SqlConnection(myCompanyDBcs);
                SqlCommand cmd = new SqlCommand("sp_userChangePass", con);
                var passwordHasher = new PasswordHasher<string>();
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter ID_SqlParam = new SqlParameter("@ID", SqlDbType.Int);
                ID_SqlParam.Value = id;
                cmd.Parameters.Add(ID_SqlParam);
                SqlParameter password_SqlParam = new SqlParameter("@password", SqlDbType.Char, 64);
                password_SqlParam.Value = passwordHasher.HashPassword(null, user.password);
                cmd.Parameters.Add(password_SqlParam);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                return RedirectToPage(GetSession());
                /*if(user1.typeID != 1)
                    return RedirectToPage("/Users/Index"); 
                else
                    return RedirectToPage("/Users/Profile"); */
            }
            else
            {
                Message = "Podane has³a musz¹ byæ takie same";
                return Page();
            }

        }
    }
}

