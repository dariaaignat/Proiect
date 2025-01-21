using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace IgnatDariaLaboratoryAnalysis.Models
{
    public class ClientModel
    {
        [Key]
        [ForeignKey("User")]
        public int UserId { get; set; }

        [Required]
        [StringLength(100)]
        public required string Name { get; set; }

        [Required]
        [StringLength(255)]
        public required string Address { get; set; }

        [Required]
        [StringLength(20)]
        public required string TelephoneNumber { get; set; }

        [StringLength(100)]
        public string? WorkPoint { get; set; }

        [StringLength(100)]
        public string? DelegateName { get; set; }

        public required UserModel User { get; set; }
        public ICollection<OrderModel>? Orders { get; set; }
    }

}
