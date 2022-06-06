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
        public string email { get; set; }
        [Display(Name = "Hasło")]
        [Required(ErrorMessage = "Pole jest obowiązkowe")]
        [DataType(DataType.Password)]
        public string password { get; set; }
        public int typeID { get; set; }
        public SiteUser()
        {

        }
        public SiteUser(int id,string user,string email,int typeid)
        {
            this.id = id;
            userName = user;
            this.email = email;
            typeID = typeid;      
        }
    }
}


