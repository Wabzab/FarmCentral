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
    public class DetailsModel : PageModel
    {
        private readonly FarmCentral.Data.FarmCentralDbContext _context;

        public DetailsModel(FarmCentral.Data.FarmCentralDbContext context)
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

            var product = await _context.Products
                                .Include(p => p.Farmer)
                                .Include(p => p.Type)
                                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }
            var farmer = await _context.Farmers.FirstOrDefaultAsync(f => f.FarmerId == farmerId);
            if (farmer == null)
            {
                return NotFound();
            }
            Product = product;
            Farmer = farmer;
            return Page();
        }
    }
}
