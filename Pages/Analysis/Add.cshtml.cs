using IgnatDariaLaboratoryAnalysis.Database;
using IgnatDariaLaboratoryAnalysis.Models;
using IgnatDariaLaboratoryAnalysis.Pages.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IgnatDariaLaboratoryAnalysis.Pages.Analysis
{
    public class AddModel : PageModel
    {
        private readonly LabDbContext _context;

        public AddModel(LabDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public AddAnalysis Analysis { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var analysis = new AnalysisModel
                {
                    Name = Analysis.Name,
                    Description = Analysis.Description,
                    CultureEnvironment = Analysis.CultureEnvironment,
                    ThermostaticTemperature = Analysis.ThermostaticTemperature,
                    CompletionDays = Analysis.CompletionDays
                };

                _context.Analyses.Add(analysis);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return RedirectToPage("./View");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                var innerMessage = ex.InnerException?.Message ?? ex.Message;
                ModelState.AddModelError("", $"Failed to add analysis: {innerMessage}");
                return Page();
            }
        }
    }
}
