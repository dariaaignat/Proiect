using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace IgnatDariaLaboratoryAnalysis.Models
{
    public class EmployeeModel
    {
        [Key]
        [ForeignKey("User")]
        public int UserId { get; set; }

        [Required]
        [StringLength(50)]
        public required string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public required string LastName { get; set; }
        public required UserModel User { get; set; }
        public ICollection<OrderModel>? Orders { get; set; }

        public string FullName => $"{FirstName} {LastName}";
    }
}
