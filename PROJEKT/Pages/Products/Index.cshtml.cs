using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PROJEKT.Data;
using PROJEKT.Models;

namespace PROJEKT.Pages.Products
{
    public class IndexModel : Session
    {
        private readonly PROJEKT.Data.CompanyPROJEKTContext _context;

        public IndexModel(PROJEKT.Data.CompanyPROJEKTContext context)
        {
            _context = context;
        }

        public IList<Product> Product { get;set; }

        public async Task OnGetAsync()
        {
            // Product = await _context.Product.ToListAsync();
           Product = await _context.Product.Include(pk => pk.Categories)
                 .ThenInclude(c => c.Category).ToListAsync();
           SetSession();
        }
    }
}
