using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using PROJEKT.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PROJEKT.Pages.Users
{
    [Authorize]
    public class EditModel : Session
    {
        public string Message { get; set; }
        [BindProperty]
        public SiteUser user { get; set; }
        public SiteUser user1 { get; set; }
        public List<TypeUser> types { get; set; }
        public IActionResult OnGet(int id)
        {
            user = DataBase.GetUser(id);
            user1 = DataBase.GetUser(User.Identity.Name);
            if (user.id != user1.id && user1.typeID != 1)
            {
                return RedirectToPage("/Account/AccessDenied");
            }
            types = DataBase.ReadTypes();
            return Page();
        }
        public async Task<IActionResult> OnPost(int id)
        {
            if (!ModelState.IsValid)
            {
                types = DataBase.ReadTypes();
                user = DataBase.GetUser(id);
                return Page();
            }
            user1 = DataBase.GetUser(id);
            user.isActive = true;
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
            if(user1.userName==User.Identity.Name)
            {
                string nametype = "Klient";
                types = DataBase.ReadTypes();
                foreach (var t in types)
                {
                    if (t.id == user.typeID)
                    {
                        nametype = t.name;
                    }
                }
                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, user.userName),
                    new Claim(ClaimTypes.Role,nametype)
                };
                var claimsIdentity = new ClaimsIdentity(claims, "CookieAuthentication");
                var cookieOptions = new CookieOptions
                {
                    Expires = DateTime.Now.AddDays(1)
                };
                await HttpContext.SignInAsync("CookieAuthentication", new ClaimsPrincipal(claimsIdentity));
            }
            return RedirectToPage(GetSession());
            
        }
    }
}
