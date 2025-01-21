using System.ComponentModel.DataAnnotations;

namespace IgnatDariaLaboratoryAnalysis.Models
{
    public class RoleModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public required string Name { get; set; }
    }
}
