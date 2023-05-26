using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FarmCentral.Pages.Employee
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
        public FarmCentral.Models.Employee Employee { get; set; } = default!;


        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.Employees == null || Employee == null)
            {
                return Page();
            }

            var employee = await _context.Employees.FirstOrDefaultAsync(m => m.Name == Employee.Name);
            if (employee == null || employee.Password != Employee.Password)
            {
                return NotFound();
            }
            else
            {
                Employee = employee;
            }

            return RedirectToPage("./Index", new { id = Employee.EmployeeId });
        }
    }
}
