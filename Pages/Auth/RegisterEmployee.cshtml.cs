using IgnatDariaLaboratoryAnalysis.Database;
using IgnatDariaLaboratoryAnalysis.Models;
using IgnatDariaLaboratoryAnalysis.Pages.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IgnatDariaLaboratoryAnalysis.Pages
{
    public class RegisterEmployeeModel : PageModel
    {
        private readonly LabDbContext _context;

        public RegisterEmployeeModel(LabDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public RegisterEmployee Input { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var user = new UserModel
                {
                    Email = Input.Email,
                    Password = BCrypt.Net.BCrypt.HashPassword(Input.Password)
                };
                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                var employee = new EmployeeModel
                {
                    UserId = user.Id,
                    FirstName = Input.FirstName,
                    LastName = Input.LastName,
                    User = user
                };
                _context.Employees.Add(employee);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return RedirectToPage("Privacy");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                ModelState.AddModelError("", $"Registration failed: {ex.InnerException?.Message ?? ex.Message}");
                return Page();
            }
        }
    }
}
