using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PROJEKT.Data;
using PROJEKT.Models;

namespace PROJEKT.Pages.Products
{
    [Authorize(Roles="Administrator,Kierownik")]
    public class EditModel : Session
    {
        private readonly PROJEKT.Data.CompanyPROJEKTContext _context;

        public EditModel(PROJEKT.Data.CompanyPROJEKTContext context, IConfiguration configuration)
        {
            _context = context;
            DataBase.configuration = configuration;
        }

        [BindProperty]
        public Product Product { get; set; }
        public List<Category> Categories { get; set; }
        [BindProperty]
        public List<int> listID { get; set; }
        List<SiteUser> users;
        int typeID { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            users = DataBase.ReadUser();
            foreach(var u in users)
            {
                if(u.userName== User.Identity.Name)
                {
                    typeID = u.typeID;
                }
            }
            if (id == null)
            {
                return NotFound();
            }
            Categories = await _context.Category.ToListAsync();
            Product = await _context.Product.Include(pk => pk.Categories).ThenInclude(c => c.Category).FirstOrDefaultAsync(m => m.id == id);

            if (Product == null)
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            foreach (var pc in _context.ProductCategory.ToList())
            {
                if (pc.ProductID == Product.id) _context.Remove(pc);
            }
            foreach (var id in listID)
            {
                var ProductandCategory = new ProductCategory()
                {
                    ProductID = Product.id,
                    CategoryID = id
                };
                _context.ProductCategory.Add(ProductandCategory);
            }

            _context.Attach(Product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(Product.id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.id == id);
        }
    }
}
