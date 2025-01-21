using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace IgnatDariaLaboratoryAnalysis.Models
{
    public class AnalysisModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public required string Name { get; set; }

        public required string Description { get; set; }

        [StringLength(255)]
        [Column("culture_environment")]
        public required string CultureEnvironment { get; set; }

        [Column("thermostatic_temperature", TypeName = "decimal(5,2)")]
        public decimal? ThermostaticTemperature { get; set; }

        [Column("completion_days")]
        public int? CompletionDays { get; set; }
        public ICollection<AnalysisOrderModel>? AnalysisOrders { get; set; }
    }
}
