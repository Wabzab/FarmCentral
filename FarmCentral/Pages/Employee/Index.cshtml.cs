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
    public class IndexModel : PageModel
    {
        private readonly FarmCentral.Data.FarmCentralDbContext _context;

        public IndexModel(FarmCentral.Data.FarmCentralDbContext context)
        {
            _context = context;
        }

        // List of farmers to display
        public IList<FarmCentral.Models.Farmer> Farmer { get;set; } = default!;

        // Retrieves all farmers to display
        public async Task OnGetAsync()
        {
            if (_context.Farmers != null)
            {
                Farmer = await _context.Farmers.ToListAsync();
            }
        }
    }
}
