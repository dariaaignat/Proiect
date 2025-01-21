using System.ComponentModel.DataAnnotations;

namespace IgnatDariaLaboratoryAnalysis.Pages.Models
{
    public class RegisterClient
    {
        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6)]
        [DataType(DataType.Password)]
        public required string Password { get; set; }

        [Required]
        [StringLength(100)]
        public required string Name { get; set; }

        [Required]
        [StringLength(255)]
        public required string Address { get; set; }

        [Required]
        [Phone]
        [StringLength(20)]
        public required string TelephoneNumber { get; set; }

        [StringLength(100)]
        public string? WorkPoint { get; set; }

        [StringLength(100)]
        public string? DelegateName { get; set; }
    }
}
