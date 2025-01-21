using IgnatDariaLaboratoryAnalysis.Database;
using IgnatDariaLaboratoryAnalysis.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace IgnatDariaLaboratoryAnalysis.Pages.Clients
{
    public class EditModel : PageModel
    {
        private readonly LabDbContext _context;

        public EditModel(LabDbContext context)
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
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var clientToUpdate = await _context.Clients
                .Include(c => c.User)
                .FirstOrDefaultAsync(c => c.UserId == Client.UserId);

            if (clientToUpdate == null)
            {
                return NotFound();
            }

            clientToUpdate.Name = Client.Name;
            clientToUpdate.Address = Client.Address;
            clientToUpdate.TelephoneNumber = Client.TelephoneNumber;
            clientToUpdate.WorkPoint = Client.WorkPoint;
            clientToUpdate.DelegateName = Client.DelegateName;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientExists(Client.UserId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./View");
        }

        private bool ClientExists(int id)
        {
            return _context.Clients.Any(e => e.UserId == id);
        }
    }
}