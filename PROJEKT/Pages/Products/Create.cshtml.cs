using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PROJEKT.Data;
using PROJEKT.Models;

namespace PROJEKT.Pages.Products
{
    [Authorize(Roles = "Administrator,Kierownik,Pracownik")]
    public class CreateModel : PageModel
    {

        private readonly PROJEKT.Data.CompanyPROJEKTContext _context;

        public CreateModel(PROJEKT.Data.CompanyPROJEKTContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Product Product { get; set; }
        public List<Category> Categories { get; set; }
        [BindProperty]
        public List<int> listID { get; set; }
        public async Task<IActionResult> OnGet()
        {
            Categories = await _context.Category.ToListAsync();
            return Page();
        }
        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Product.Add(Product);
            await _context.SaveChangesAsync();
            foreach (var id in listID)
            {
                var ProductCategory = new ProductCategory()
                {
                    ProductID = Product.id,
                    CategoryID = id
                };
                _context.ProductCategory.Add(ProductCategory);
            }
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
    }
}
