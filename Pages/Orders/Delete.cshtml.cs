using IgnatDariaLaboratoryAnalysis.Database;
using IgnatDariaLaboratoryAnalysis.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IgnatDariaLaboratoryAnalysis.Pages.Orders
{
    public class DeleteModel : PageModel
    {
        private readonly LabDbContext _context;

        public DeleteModel(LabDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public OrderModel Order { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Order = await _context.Orders
                .Include(o => o.Client)
                .Include(o => o.Employee)
                .Include(o => o.AnalysisOrders)
                    .ThenInclude(ao => ao.Analysis)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (Order == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Order = await _context.Orders
                .Include(o => o.AnalysisOrders)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (Order != null)
            {
                _context.AnalysisOrders.RemoveRange(Order.AnalysisOrders);
                _context.Orders.Remove(Order);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./View");
        }
    }
}