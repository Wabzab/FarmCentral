using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FarmCentral.Data;
using FarmCentral.Models;

namespace FarmCentral.Pages.Farmer
{
    public class EditModel : PageModel
    {
        private readonly FarmCentral.Data.FarmCentralDbContext _context;

        public EditModel(FarmCentral.Data.FarmCentralDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Product Product { get; set; } = default!;
        [BindProperty]
        public FarmCentral.Models.Farmer Farmer { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id, int farmerId)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product =  await _context.Products.FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }
            var farmer = await _context.Farmers.FirstOrDefaultAsync(m => m.FarmerId == farmerId);
            if (farmer == null)
            {
                return NotFound();
            }
            Product = product;
            Farmer = farmer;
            ViewData["TypeId"] = new SelectList(_context.Types, "TypeId", "Name");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            _context.Attach(Product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(Product.ProductId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index", new { farmerId = Product.FarmerId });
        }

        private bool ProductExists(int id)
        {
            return (_context.Products?.Any(e => e.ProductId == id)).GetValueOrDefault();
        }
    }
}
