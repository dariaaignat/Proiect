using System.ComponentModel.DataAnnotations.Schema;

namespace IgnatDariaLaboratoryAnalysis.Models
{
    public class AnalysisOrderModel
    {
        [Column("analysis_id")]
        public int AnalysisId { get; set; }

        [Column("order_id")]
        public int OrderId { get; set; }

        public AnalysisModel Analysis { get; set; }
        public OrderModel Order { get; set; }
    }
}
