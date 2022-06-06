using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using PROJEKT.Models;

namespace PROJEKT.Pages.Login
{
    public class RegisterModel : PageModel
    {
        private readonly IConfiguration _configuration;
        public string Message { get; set; }
        [BindProperty]
        public SiteUser user { get; set; }
        [DisplayName("Powtórz hasło")]
        [DataType(DataType.Password)]
        public string password { get; set; }
        public List<TypeUser> types { get; set; }
        StringBuilder htmlStr = new StringBuilder("");
        public RegisterModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void OnGet()
        {
            types = DataBase.ReadTypes(_configuration);
        }
        public IActionResult OnPost(string password)
        {
            types = DataBase.ReadTypes(_configuration);
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var users = DataBase.ReadUser(_configuration);
            foreach(var u in users) 
            if(u.userName==user.userName)
            {
                    htmlStr.Append("!!! Nazwa użytkownika juz istnieje");
                    if (password != user.password)
                        htmlStr.Append("<br/>!!! Podane hasła muszą być takie same");
                    Message = htmlStr.ToString(); 
                    return Page();
            }
            if (password == user.password)
            {
                string myCompanyDBcs = _configuration.GetConnectionString("myCompanyDB");
                SqlConnection con = new SqlConnection(myCompanyDBcs);
                SqlCommand cmd = new SqlCommand("sp_userAdd", con);
                var passwordHasher = new PasswordHasher<string>();
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter userName_SqlParam = new SqlParameter("@userName", SqlDbType.Char, 64);
                userName_SqlParam.Value = user.userName;
                cmd.Parameters.Add(userName_SqlParam);
                SqlParameter email_SqlParam = new SqlParameter("@email", SqlDbType.Char, 64);
                email_SqlParam.Value = user.email;
                cmd.Parameters.Add(email_SqlParam);
                SqlParameter password_SqlParam = new SqlParameter("@password", SqlDbType.Char, 64);
                //string pas = passwordHasher.HashPassword(null, user.password);
                password_SqlParam.Value = passwordHasher.HashPassword(null, user.password);
                cmd.Parameters.Add(password_SqlParam);
                SqlParameter type_SqlParam = new SqlParameter("@typeID", SqlDbType.Int, 64);
                if(user.typeID!=0)
                    type_SqlParam.Value = user.typeID;
                else
                    type_SqlParam.Value = 4;
                cmd.Parameters.Add(type_SqlParam);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                return RedirectToPage("/Products/Index"); 
            }
            else
            {
                Message = "!!! Podane hasła muszą być takie same";
                return Page();
            }
        }
    }
}