using IgnatDariaLaboratoryAnalysis.Database;
using IgnatDariaLaboratoryAnalysis.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace IgnatDariaLaboratoryAnalysis.Pages.Orders
{
    public class ViewModel : PageModel
    {
        private readonly LabDbContext _context;

        public ViewModel(LabDbContext context)
        {
            _context = context;
        }

        public IList<OrderModel> Orders { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Orders = await _context.Orders
                .Include(o => o.Client)
                .Include(o => o.Employee)
                .Include(o => o.AnalysisOrders)
                    .ThenInclude(ao => ao.Analysis)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();
        }
    }
}