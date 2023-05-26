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

namespace FarmCentral.Pages.Employee
{
    public class CreateModel : PageModel
    {
        private readonly FarmCentral.Data.FarmCentralDbContext _context;
        public String errorString;

        public CreateModel(FarmCentral.Data.FarmCentralDbContext context)
        {
            _context = context;
            errorString = string.Empty;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public FarmCentral.Models.Farmer Farmer { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.Farmers == null || Farmer == null)
            {
                errorString = "Invalid data!";
                return Page();
            }

            var farmer = await _context.Farmers.FirstOrDefaultAsync(f => f.Name == Farmer.Name);
            if (farmer != null)
            {
                errorString = "Farmer name taken!";
                return Page();
            }

            _context.Farmers.Add(Farmer);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
