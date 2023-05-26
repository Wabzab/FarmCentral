using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using FarmCentral.Data;
using FarmCentral.Models;
using Microsoft.EntityFrameworkCore;

namespace FarmCentral.Pages.Farmer
{
    public class CreateModel : PageModel
    {
        private readonly FarmCentral.Data.FarmCentralDbContext _context;

        public CreateModel(FarmCentral.Data.FarmCentralDbContext context)
        {
            _context = context;
        }


        [BindProperty]
        public Product Product { get; set; } = default!;
        [BindProperty]
        public FarmCentral.Models.Farmer Farmer { get; set; } = default!;


        public async Task OnGet(int farmerId)
        {
            var farmer = await _context.Farmers.FirstOrDefaultAsync(f => f.FarmerId == farmerId);
            if (farmer != null)
            {
                Farmer = farmer;
            }
            ViewData["TypeId"] = new SelectList(_context.Types, "TypeId", "Name");
        }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (_context.Products == null || Product == null)
            {
                return Page();
            }

            _context.Products.Add(Product);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index", new { farmerId = Farmer.FarmerId });
        }
    }
}
