using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FarmCentral.Data;
using FarmCentral.Models;

namespace FarmCentral.Pages.Employee
{
    public class DeleteModel : PageModel
    {
        private readonly FarmCentral.Data.FarmCentralDbContext _context;

        public DeleteModel(FarmCentral.Data.FarmCentralDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public FarmCentral.Models.Farmer Farmer { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Farmers == null)
            {
                return NotFound();
            }

            var farmer = await _context.Farmers.FirstOrDefaultAsync(m => m.FarmerId == id);

            if (farmer == null)
            {
                return NotFound();
            }
            else 
            {
                Farmer = farmer;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Farmers == null)
            {
                return NotFound();
            }
            var farmer = await _context.Farmers.FindAsync(id);

            if (farmer != null)
            {
                Farmer = farmer;
                _context.Farmers.Remove(Farmer);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
