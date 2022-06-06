using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PROJEKT.Models
{
    public class Product
    {
        [Key]
        [Display(Name = "ID")]
        public int id { get; set; }

        [Display(Name = "Nazwa")]
        [Required(ErrorMessage = "Pole jest obowiązkowe")]
        public string name { get; set; }

        [Display(Name = "Cena")]
        [Required(ErrorMessage = "Pole jest obowiązkowe")]
        //[DataType(DataType.Currency, ErrorMessage ="Cena musi przyjmować wartość ceny")]
        [Range(0.01,double.MaxValue,ErrorMessage="Cena musi być większa od 0")]
        public decimal price { get; set; }

        [Display(Name = "Opis")]
        public string opis { get; set; }
        [Display(Name = "Kategorie")]
        public ICollection<ProductCategory> Categories { get; set; }
        public Product()
        {

        }
        public Product(int id, string name, decimal price,string opis)
        {
            this.id = id;
            this.name = name;
            this.price = price;
            this.opis = opis;
        }

    }
}
