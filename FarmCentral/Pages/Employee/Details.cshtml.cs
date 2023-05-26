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
using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;

namespace FarmCentral.Pages.Employee
{
    public class DetailsModel : PageModel
    {
        private readonly FarmCentral.Data.FarmCentralDbContext _context;

        public DetailsModel(FarmCentral.Data.FarmCentralDbContext context)
        {
            _context = context;
        }

        // Same as '~/Pages/Farmer/Index'
        public FarmCentral.Models.Farmer Farmer { get; set; } = default!; 
        public IList<FarmCentral.Models.Product> Products { get; set; } = default!;

        [BindProperty, DataType(DataType.Date)]
        public DateTime? StartDate { get; set; }
        [BindProperty, DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }
        [BindProperty]
        public int? FilterType { get; set; }

        public async Task OnGetAsync(int id)
        {
            if (_context.Farmers != null && _context.Products != null)
            {
                var farmer = await _context.Farmers.FirstOrDefaultAsync(m => m.FarmerId == id);
                if (farmer != null)
                {
                    Farmer = farmer;
                    Products = await _context.Products
                        .Include(p => p.Farmer)
                        .Include(p => p.Type)
                        .Where(p => p.FarmerId == id)
                        .ToListAsync();
                }
                ViewData["TypeId"] = GetTypes();
            }
        }


        public async Task OnPostAsync(DateTime? StartDate, DateTime? EndDate, int? FilterType, int farmerId)
        {
            await FilterProducts(StartDate, EndDate, FilterType, farmerId);
        }

        public async Task FilterProducts(DateTime? StartDate, DateTime? EndDate, int? FilterType, int farmerId)
        {
            if (StartDate == null)
            {
                StartDate = DateTime.MinValue;
            }
            if (EndDate == null)
            {
                EndDate = DateTime.MaxValue;
            }
            if (StartDate > EndDate)
            {
                StartDate = EndDate;
            }

            var farmer = await _context.Farmers.FirstOrDefaultAsync(f => f.FarmerId == farmerId);
            if (farmer != null)
            {
                Farmer = farmer;
            }
            var product = _context.Products
                .Include(p => p.Farmer)
                .Include(p => p.Type)
                .Where(
                p => p.FarmerId == farmerId 
                && p.Date >= StartDate 
                && p.Date <= EndDate);

            if (FilterType != null)
            {
                product = product.Where(p => p.TypeId == FilterType);
            }

            Products = await product.ToListAsync();
            ViewData["TypeId"] = GetTypes();
        }

        private List<SelectListItem> GetTypes()
        {
            List<SelectListItem> typesList = _context.Types
                .Select(t => new SelectListItem()
                {
                    Value = t.TypeId.ToString(),
                    Text = t.Name
                })
                .ToList();
            //new SelectList(_context.Types, "TypeId", "Name");
            typesList.Insert(0, new SelectListItem
            {
                Value = "",
                Text = "--- Filter By Type ---"
            });
            return typesList;
        }

    }
}
