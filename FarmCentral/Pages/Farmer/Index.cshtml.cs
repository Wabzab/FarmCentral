using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FarmCentral.Data;
using FarmCentral.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace FarmCentral.Pages.Farmer
{
    public class IndexModel : PageModel
    {
        private readonly FarmCentral.Data.FarmCentralDbContext _context;

        public IndexModel(FarmCentral.Data.FarmCentralDbContext context)
        {
            _context = context;
        }

        // Keep track of current farmer and products on display
        public FarmCentral.Models.Farmer Farmer { get; set; } = default!;
        public IList<Product> Products { get;set; } = default!;

        // Bound properties for filtering products
        [BindProperty, DataType(DataType.Date)]
        public DateTime? StartDate { get; set; }
        [BindProperty, DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }
        [BindProperty]
        public int? FilterType { get; set; }

        // Called when page is loaded
        // farmerId is routed along with this page always
        public async Task OnGetAsync(int farmerId)
        {
            if (_context.Products != null)
            {
                // Assign farmer and retrieve all products
                var farmer = await _context.Farmers.FirstOrDefaultAsync(f => f.FarmerId == farmerId);
                if (farmer != null)
                {
                    Farmer = farmer;
                    Products = await _context.Products
                    .Include(p => p.Farmer)
                    .Include(p => p.Type)
                    .Where(p => p.FarmerId == farmer.FarmerId)
                    .ToListAsync();
                }
                // Get types to filter by
                ViewData["TypeId"] = GetTypes();
            }
        }


        public async Task OnPostAsync(DateTime? StartDate, DateTime? EndDate, int? FilterType, int farmerId)
        {
            await FilterProducts(StartDate, EndDate, FilterType, farmerId);
        }

        // Reassigns 'Products' to new filtered list
        public async Task FilterProducts(DateTime? StartDate, DateTime? EndDate, int? FilterType, int farmerId)
        {
            // Handle null date values
            if (StartDate == null)
            {
                StartDate = DateTime.MinValue;
            }
            if (EndDate == null)
            {
                EndDate = DateTime.MaxValue;
            }
            // Handle lower target being grater than upper target
            if (StartDate > EndDate)
            {
                StartDate = EndDate;
            }

            // Safely reassign farmer, lost on reloading page
            var farmer = await _context.Farmers.FirstOrDefaultAsync(f => f.FarmerId == farmerId);
            if (farmer != null) {
                Farmer = farmer;
            }
            // Filter products by date range
            var product = _context.Products
                .Include(p => p.Farmer)
                .Include(p => p.Type)
                .Where(
                p => p.FarmerId == farmerId
                && p.Date >= StartDate
                && p.Date <= EndDate);
            // Filter further by type
            if (FilterType != null)
            {
                product = product.Where(p => p.TypeId == FilterType);
            }

            // Reassign products and retrieve types again
            Products = await product.ToListAsync();
            ViewData["TypeId"] = GetTypes();
        }

        // Returns the list of product types with default null option
        private List<SelectListItem> GetTypes()
        {
            List<SelectListItem> typesList = _context.Types
                .Select(t => new SelectListItem()
                {
                    Value = t.TypeId.ToString(),
                    Text = t.Name
                })
                .ToList();
            // This adds the default null option
            typesList.Insert(0, new SelectListItem
            {
                Value = "",
                Text = "--- Filter By Type ---"
            });
            return typesList;
        }
    }
}
