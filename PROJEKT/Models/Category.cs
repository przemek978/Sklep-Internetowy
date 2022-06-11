using Microsoft.AspNetCore.Mvc;
using PROJEKT.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PROJEKT.Models
{
    public class Category
    {
        [Key]
        [Display(Name = "ID")]
        public int id { get; set; }
        [Display(Name = "Krótka nazwa")]
        [Required(ErrorMessage = "Pole jest obowiązkowe")]
        public string shortName { get; set; }
        [Display(Name = "Długa nazwa")]
        [Required(ErrorMessage = "Pole jest obowiązkowe")]
        public string longName { get; set; }
        [Display(Name = "Opis")]
        [Required(ErrorMessage = "Pole jest obowiązkowe")]
        public string opis { get; set; }
        [Display(Name = "Produkty")]
        public ICollection<ProductCategory> Products { get; set; }

    }
}
