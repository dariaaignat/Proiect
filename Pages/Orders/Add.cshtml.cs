using IgnatDariaLaboratoryAnalysis.Database;
using IgnatDariaLaboratoryAnalysis.Models;
using IgnatDariaLaboratoryAnalysis.Pages.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace IgnatDariaLaboratoryAnalysis.Pages.Orders
{
    public class AddModel : PageModel
    {
        private readonly LabDbContext _context;
        public AddModel(LabDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public AddOrder Order { get; set; } = default!;
        public SelectList ClientList { get; set; } = default!;
        public SelectList EmployeeList { get; set; } = default!;
        public SelectList AnalysisList { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            ClientList = new SelectList(
                await _context.Clients.OrderBy(c => c.Name).ToListAsync(),
                "UserId", "Name");
            EmployeeList = new SelectList(
                await _context.Employees.OrderBy(e => e.LastName).ToListAsync(),
                "UserId",
                "FullName");
            AnalysisList = new SelectList(
                await _context.Analyses.OrderBy(a => a.Name).ToListAsync(),
                "Id",
                "Name");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await LoadDropDowns();
                return Page();
            }

            var newOrder = new OrderModel
            {
                ClientId = Order.ClientId,
                EmployeeId = Order.EmployeeId,
                OrderDate = Order.OrderDate,
                SampleName = Order.SampleName,
                NumberOfSamples = Order.NumberOfSamples,
                SampleManufactureDate = Order.SampleManufactureDate,
                SampleExpirationDate = Order.SampleExpirationDate,
                AnalysisOrders = Order.SelectedAnalysisIds.Select(analysisId =>
                    new AnalysisOrderModel
                    {
                        AnalysisId = analysisId
                    }).ToList()
            };

            _context.Orders.Add(newOrder);
            await _context.SaveChangesAsync();
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
    }
}