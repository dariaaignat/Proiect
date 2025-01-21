// Edit.cshtml.cs
using IgnatDariaLaboratoryAnalysis.Database;
using IgnatDariaLaboratoryAnalysis.Models;
using IgnatDariaLaboratoryAnalysis.Pages.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace IgnatDariaLaboratoryAnalysis.Pages.Orders
{
    public class EditModel : PageModel
    {
        private readonly LabDbContext _context;

        public EditModel(LabDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public EditOrder Order { get; set; } = default!;
        public SelectList ClientList { get; set; } = default!;
        public SelectList EmployeeList { get; set; } = default!;
        public SelectList AnalysisList { get; set; } = default!;

        public async Task<IActionResult>
    OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
            .Include(o => o.AnalysisOrders)
            .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
            {
                return NotFound();
            }

            Order = new EditOrder
            {
                Id = order.Id,
                ClientId = order.ClientId,
                EmployeeId = order.EmployeeId,
                OrderDate = order.OrderDate,
                SampleName = order.SampleName,
                NumberOfSamples = order.NumberOfSamples,
                SampleManufactureDate = order.SampleManufactureDate,
                SampleExpirationDate = order.SampleExpirationDate,
                SelectedAnalysisIds = order.AnalysisOrders.Select(ao => ao.AnalysisId).ToList()
            };

            await LoadDropDowns();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await LoadDropDowns();
                return Page();
            }

            var orderToUpdate = await _context.Orders
            .Include(o => o.AnalysisOrders)
            .FirstOrDefaultAsync(o => o.Id == Order.Id);

            if (orderToUpdate == null)
            {
                return NotFound();
            }

            orderToUpdate.ClientId = Order.ClientId;
            orderToUpdate.EmployeeId = Order.EmployeeId;
            orderToUpdate.OrderDate = Order.OrderDate;
            orderToUpdate.SampleName = Order.SampleName;
            orderToUpdate.NumberOfSamples = Order.NumberOfSamples;
            orderToUpdate.SampleManufactureDate = Order.SampleManufactureDate;
            orderToUpdate.SampleExpirationDate = Order.SampleExpirationDate;

            _context.AnalysisOrders.RemoveRange(orderToUpdate.AnalysisOrders);
            orderToUpdate.AnalysisOrders = Order.SelectedAnalysisIds.Select(analysisId =>
            new AnalysisOrderModel
            {
                OrderId = orderToUpdate.Id,
                AnalysisId = analysisId
            }).ToList();

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(Order.Id))
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

        private async Task LoadDropDowns()
        {
            ClientList = new SelectList(
            await _context.Clients.OrderBy(c => c.Name).ToListAsync(),
            "UserId", "Name", Order.ClientId);

            EmployeeList = new SelectList(
            await _context.Employees.OrderBy(e => e.LastName).ToListAsync(),
            "UserId", "FullName", Order.EmployeeId);

            AnalysisList = new SelectList(
            await _context.Analyses.OrderBy(a => a.Name).ToListAsync(),
            "Id", "Name", Order.SelectedAnalysisIds);
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }
    }
}
