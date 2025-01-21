
using IgnatDariaLaboratoryAnalysis.Database;
using IgnatDariaLaboratoryAnalysis.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace IgnatDariaLaboratoryAnalysis.Pages.Clients
{
    public class ViewModel : PageModel
    {
        private readonly LabDbContext _context;

        public ViewModel(LabDbContext context)
        {
            _context = context;
        }

        public IList<ClientModel> Clients { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Clients = await _context.Clients
                .Include(c => c.User)
                .OrderBy(c => c.Name)
                .ToListAsync();
        }
    }
}