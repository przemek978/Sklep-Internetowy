using System.ComponentModel.DataAnnotations;

namespace PROJEKT.Models
{
    public class TypeUser
    {
        [Display(Name = "ID")]
        public int id { get; set; }
        [Display(Name = "Typ uprawnień")]
        public string name { get; set; }
        public TypeUser(int id,string name)
        {
            this.id = id;   
            this.name = name;  
        }
    }
}
