using IgnatDariaLaboratoryAnalysis.Database;
using IgnatDariaLaboratoryAnalysis.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace IgnatDariaLaboratoryAnalysis.Pages.Analysis
{
    public class ViewModel : PageModel
    {
        private readonly LabDbContext _context;

        public ViewModel(LabDbContext context)
        {
            _context = context;
        }

        public IList<AnalysisModel> Analyses { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Analyses != null)
            {
                Analyses = await _context.Analyses.ToListAsync();
            }
        }
    }
}