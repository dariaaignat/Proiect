using System.ComponentModel.DataAnnotations;

namespace IgnatDariaLaboratoryAnalysis.Models
{
    public class UserModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        [StringLength(255)]
        public required string Password { get; set; }

        public ClientModel? Client { get; set; }
        public EmployeeModel? Employee { get; set; }
    }
}
