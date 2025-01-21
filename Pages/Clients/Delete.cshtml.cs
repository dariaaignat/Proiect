
using IgnatDariaLaboratoryAnalysis.Database;
using IgnatDariaLaboratoryAnalysis.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace IgnatDariaLaboratoryAnalysis.Pages.Clients
{
    public class DeleteModel : PageModel
    {
        private readonly LabDbContext _context;

        public DeleteModel(LabDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ClientModel Client { get; set; } = null!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Clients
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.UserId == id);

            if (client == null)
            {
                return NotFound();
            }

            Client = client;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                var client = await _context.Clients
                    .Include(c => c.User)
                    .FirstOrDefaultAsync(m => m.UserId == Client.UserId);

                if (client == null)
                {
                    return NotFound();
                }

                _context.Clients.Remove(client);
                await _context.SaveChangesAsync();

            
                if (client.User != null)
                {
                    _context.Users.Remove(client.User);
                    await _context.SaveChangesAsync();
                }

                return RedirectToPage("./View");
            }
            catch (Exception ex)
            {
       
                ModelState.AddModelError(string.Empty, "An error occurred while deleting the client: " + ex.Message);
                return Page();
            }
        }
    }
}