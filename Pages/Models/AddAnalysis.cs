using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IgnatDariaLaboratoryAnalysis.Pages.Models
{
    public class AddAnalysis
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100)]
        [Display(Name = "Analysis Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Culture environment is required")]
        [StringLength(255)]
        [Display(Name = "Culture Environment")]
        public string CultureEnvironment { get; set; }

        [Display(Name = "Thermostatic Temperature")]
        [Column(TypeName = "decimal(5,2)")]
        [Range(0, 100, ErrorMessage = "Temperature must be between 0 and 100")]
        public decimal? ThermostaticTemperature { get; set; }

        [Display(Name = "Days to Complete")]
        [Range(1, 365, ErrorMessage = "Number of days must be between 1 and 365")]
        public int? CompletionDays { get; set; }
    }
}