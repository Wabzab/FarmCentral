using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FarmCentral.Data;
using FarmCentral.Models;

namespace FarmCentral.Pages.Farmer
{
    public class DeleteModel : PageModel
    {
        private readonly FarmCentral.Data.FarmCentralDbContext _context;

        public DeleteModel(FarmCentral.Data.FarmCentralDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Product Product { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                                .Include(p => p.Farmer)
                                .Include(p => p.Type)
                                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }
            else 
            {
                Product = product;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }
            var product = await _context.Products.FindAsync(id);

            if (product != null)
            {
                Product = product;
                _context.Products.Remove(Product);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index", new { farmerId = product.FarmerId });
        }
    }
}
