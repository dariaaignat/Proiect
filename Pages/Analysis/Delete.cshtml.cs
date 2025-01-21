using IgnatDariaLaboratoryAnalysis.Database;
using IgnatDariaLaboratoryAnalysis.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace IgnatDariaLaboratoryAnalysis.Pages.Analysis
{
    public class DeleteModel : PageModel
    {
        private readonly LabDbContext _context;

        public DeleteModel(LabDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public AnalysisModel Analysis { get; set; } = default!;

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

            Analysis = analysis;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var analysis = await _context.Analyses.FindAsync(id);
            if (analysis != null)
            {
                Analysis = analysis;
                _context.Analyses.Remove(Analysis);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./View");
        }
    }
}