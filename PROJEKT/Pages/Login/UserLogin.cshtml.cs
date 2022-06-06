using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Security.Claims;
using System.Threading.Tasks;
using PROJEKT.Models;

namespace PROJEKT.Pages.Login
{
    public class UserLoginModel : PageModel
    {
        private readonly IConfiguration _configuration;
        public string Message { get; set; }
        [BindProperty]
        public SiteUser user { get; set; }
        public List<TypeUser> types { get; set; }
        public List<SiteUser> users { get; set; }
        public UserLoginModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void OnGet()
        {
            types = DataBase.ReadTypes(_configuration);
        }
        private bool ValidateUser(SiteUser user)
        {
            string myCompanyDBcs = _configuration.GetConnectionString("myCompanyDB");
            SqlConnection con = new SqlConnection(myCompanyDBcs);
            SqlCommand cmd = new SqlCommand("sp_userDetails", con);
            cmd.CommandType = CommandType.StoredProcedure;
            var passwordHasher = new PasswordHasher<string>();
            string nazwa, haslo;
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                nazwa = reader["userName"].ToString().Trim();
                if (user.userName == nazwa)
                {
                    haslo = reader["password"].ToString().Trim();
                    if (passwordHasher.VerifyHashedPassword(null, haslo, user.password) == PasswordVerificationResult.Success)
                    {
                        reader.Close();
                        con.Close();
                        return true;
                    }
                }
            }
            reader.Close();
            con.Close();
            return false;
        }
        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            if (ValidateUser(user))
            {
                string nametype="Klient";

                types = DataBase.ReadTypes(_configuration);
                users = DataBase.ReadUser(_configuration);
                foreach(var u in users)
                {
                    if(user.userName==u.userName)
                    {
                        user.typeID= u.typeID;  
                    }
                }
                foreach (var t in types)
                {
                    if(t.id==user.typeID)
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
                await HttpContext.SignInAsync("CookieAuthentication", new ClaimsPrincipal(claimsIdentity));
                if (returnUrl != null)
                    return Redirect(returnUrl);
                else
                    return RedirectToPage("/Products/Index");
            }
            return Page();
        }
        /*private bool ValidateUser(SiteUser user)
        {
            if ((user.userName == "admin") && (user.password == "abc"))
                return true;
            else
                return false;
        }*/

    }
}
