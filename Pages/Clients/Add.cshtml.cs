
using IgnatDariaLaboratoryAnalysis.Database;
using IgnatDariaLaboratoryAnalysis.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IgnatDariaLaboratoryAnalysis.Pages.Clients
{
    public class AddModel : PageModel
    {
        private readonly LabDbContext _context;

        public AddModel(LabDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ClientModel Client { get; set; } = null!;

        public IActionResult OnGet()
        {
        
            Client = new ClientModel
            {
                Name = string.Empty,
                Address = string.Empty,
                TelephoneNumber = string.Empty,
                User = new UserModel
                {
                    Email = string.Empty,
                    Password = string.Empty
                }
            };
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Page();
                }

           
                var user = new UserModel
                {
                    Email = $"{Client.Name.ToLower().Replace(" ", "")}@laborator.com",
                    Password = "DefaultPassword123!" 
                };

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

        
                Client.UserId = user.Id;
                Client.User = user;

                _context.Clients.Add(Client);
                await _context.SaveChangesAsync();

                return RedirectToPage("./View");
            }
            catch (Exception ex)
            {
           
                ModelState.AddModelError(string.Empty, "An error occurred while saving the client: " + ex.Message);
                return Page();
            }
        }
    }
}