using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PROJEKT.Data;
using PROJEKT.Models;

namespace PROJEKT.Pages.Products
{
    public class DetailsModel : PageModel
    {
        private readonly PROJEKT.Data.CompanyPROJEKTContext _context;

        public DetailsModel(PROJEKT.Data.CompanyPROJEKTContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Product Product { get; set; }
        public List<Category> Categories { get; set; }
        /*public async Task<IActionResult> OnGet()
        {
            Categories = await _context.Category.ToListAsync();
            return Page();
        }*/
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //Categories = await _context.Category.ToListAsync();
            Product = await _context.Product.Include(pk => pk.Categories).ThenInclude(c => c.Category).FirstOrDefaultAsync(m => m.id == id);
            
            if (Product == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
