using IgnatDariaLaboratoryAnalysis.Database;
using IgnatDariaLaboratoryAnalysis.Models;
using IgnatDariaLaboratoryAnalysis.Pages.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IgnatDariaLaboratoryAnalysis.Pages
{
    public class RegisterClientModel : PageModel
    {
        private readonly LabDbContext _context;

        public RegisterClientModel(LabDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public RegisterClient Input { get; set; }

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

                var client = new ClientModel
                {
                    UserId = user.Id,
                    Name = Input.Name,
                    Address = Input.Address,
                    TelephoneNumber = Input.TelephoneNumber,
                    WorkPoint = Input.WorkPoint,
                    DelegateName = Input.DelegateName,
                    User = user
                };
                _context.Clients.Add(client);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return RedirectToPage("Privacy");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                var innerMessage = ex.InnerException?.Message ?? ex.Message;
                ModelState.AddModelError("", $"Registration failed: {innerMessage}");
                return Page();
            }
        }
    }
}