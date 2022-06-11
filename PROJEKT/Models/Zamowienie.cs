using System.ComponentModel.DataAnnotations;

namespace PROJEKT.Models
{
    public class Zamowienie
    {
        [Display(Name = "ID")]
        public int id { get; set; }
        public int UserID { get; set; }
        [Display(Name = "Produkty")]
        public string produkty { get; set; }
        public Zamowienie()
        {

        }
        public Zamowienie(int userid,string Prod)
        {
            UserID = userid;    
            produkty=Prod;
        }
        public Zamowienie(int id,int userid,string Prod)
        {
            this.id = id;
            UserID = userid;    
            produkty = Prod;
        }
    }
}
