using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PROJEKT.Models
{
    public class SiteUser
    {
        public int id { get; set; }
        [Display(Name = "Nazwa użytkownika")]
        [Required(ErrorMessage = "Pole jest obowiązkowe")]
        public string userName { get; set; }
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Pole jest obowiązkowe")]
        [RegularExpression(@"^[\w-]+(\.[\w-]+)*@([a-z0-9-]+(\.[a-z0-9-]+)*?\.[a-z]{2,6}|(\d{1,3}\.){3}\d{1,3})(:\d{4})?$", ErrorMessage = "Wartosc musi byc emailem")]
        public string email { get; set; }
        [Display(Name = "Hasło")]
        [Required(ErrorMessage = "Pole jest obowiązkowe")]
        [DataType(DataType.Password)]
        public string password { get; set; }
        public int typeID { get; set; }
        [Display(Name = "Status konta")]
        public bool isActive { get; set; }
        public SiteUser()
        {

        }
        public SiteUser(int id,string username,string email,string password,int typeid,bool a)
        {
            this.id = id;
            userName = username;
            this.email = email;
            this.password = password;
            typeID = typeid;    
            isActive = a;
        }
    }
}


