using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PROJEKT.Data;
using PROJEKT.Models;

namespace PROJEKT.Pages.Categories
{
    public class DetailsModel : Session
    {
        private readonly PROJEKT.Data.CompanyPROJEKTContext _context;

        public DetailsModel(PROJEKT.Data.CompanyPROJEKTContext context)
        {
            _context = context;
        }

        public Category Category { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Category = await _context.Category.Include(pk => pk.Products).ThenInclude(p => p.Product).FirstOrDefaultAsync(m => m.id == id);

            if (Category == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
