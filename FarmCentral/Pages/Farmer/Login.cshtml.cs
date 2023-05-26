using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FarmCentral.Pages.Farmer
{
    public class LoginModel : PageModel
    {
        private readonly FarmCentral.Data.FarmCentralDbContext _context;

        public LoginModel(FarmCentral.Data.FarmCentralDbContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
        }

        [BindProperty]
        public FarmCentral.Models.Farmer Farmer { get; set; } = default!;


        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.Farmers == null || Farmer == null)
            {
                return Page();
            }

            var farmer = await _context.Farmers.FirstOrDefaultAsync(m => m.Name == Farmer.Name);
            if (farmer == null || farmer.Password != Farmer.Password)
            {
                return NotFound();
            }

            return RedirectToPage("./Index", new { farmerId = farmer.FarmerId });
        }
    }
}
