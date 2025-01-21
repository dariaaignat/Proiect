using IgnatDariaLaboratoryAnalysis.Pages.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IgnatDariaLaboratoryAnalysis.Database;

namespace IgnatDariaLaboratoryAnalysis.Pages.Analysis
{
    public class EditModel : PageModel
    {
        private readonly LabDbContext _context;

        public EditModel(LabDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public EditAnalysis Analysis { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var analysis = await _context.Analyses.FirstOrDefaultAsync(m => m.Id == id);
            if (analysis == null)
            {
                return NotFound();
            }

            Analysis = new EditAnalysis
            {
                Id = analysis.Id,
                Name = analysis.Name,
                Description = analysis.Description,
                CultureEnvironment = analysis.CultureEnvironment,
                ThermostaticTemperature = analysis.ThermostaticTemperature,
                CompletionDays = analysis.CompletionDays
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var analysisToUpdate = await _context.Analyses.FindAsync(Analysis.Id);
            if (analysisToUpdate == null)
            {
                return NotFound();
            }

            analysisToUpdate.Name = Analysis.Name;
            analysisToUpdate.Description = Analysis.Description;
            analysisToUpdate.CultureEnvironment = Analysis.CultureEnvironment;
            analysisToUpdate.ThermostaticTemperature = Analysis.ThermostaticTemperature;
            analysisToUpdate.CompletionDays = Analysis.CompletionDays;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AnalysisExists(Analysis.Id))
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

        private bool AnalysisExists(int id)
        {
            return _context.Analyses.Any(e => e.Id == id);
        }
    }
}