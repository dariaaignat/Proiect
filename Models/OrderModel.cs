using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace IgnatDariaLaboratoryAnalysis.Models
{
    public class OrderModel
    {
        public int Id { get; set; }

        [Column("client_id")]
        public int ClientId { get; set; }

        [Column("employee_id")]
        public int EmployeeId { get; set; }

        [Required]
        [Column("order_date")]
        public required DateTime OrderDate { get; set; }

        [Required]
        [StringLength(100)]
        [Column("sample_name")]
        public required string SampleName { get; set; }

        [Required]
        [Column("number_of_sample")]
        public int NumberOfSamples { get; set; }

        [Column("sample_manufacture_date", TypeName = "date")]
        public DateTime? SampleManufactureDate { get; set; }

        [Column("sample_expiration_date", TypeName = "date")]
        public DateTime? SampleExpirationDate { get; set; }

        public ClientModel Client { get; set; }
        public EmployeeModel Employee { get; set; }

        [Column("analysis_orders")]
        public required ICollection<AnalysisOrderModel> AnalysisOrders { get; set; }
    }
}
