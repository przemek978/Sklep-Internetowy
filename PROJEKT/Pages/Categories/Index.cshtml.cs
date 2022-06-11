using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PROJEKT.Data;
using PROJEKT.Models;

namespace PROJEKT.Pages.Categories
{
    public class IndexModel : Session
    {
        private readonly PROJEKT.Data.CompanyPROJEKTContext _context;

        public IndexModel(PROJEKT.Data.CompanyPROJEKTContext context)
        {
            _context = context;
        }

        public IList<Category> Category { get;set; }

        public async Task OnGetAsync()
        {
            //Category = await _context.Category.ToListAsync();
            Category = await _context.Category.Include(pk => pk.Products)
                .ThenInclude(p => p.Product).ToListAsync();
            SetSession();
        }
    }
}
