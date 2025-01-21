using System.ComponentModel.DataAnnotations;

namespace IgnatDariaLaboratoryAnalysis.Pages.Models
{
    public class AddOrder
    {
        [Display(Name = "Client")]
        [Required(ErrorMessage = "Please select a client")]
        public int ClientId { get; set; }

        [Display(Name = "Employee")]
        [Required(ErrorMessage = "Please select an employee")]
        public int EmployeeId { get; set; }

        [Display(Name = "Order Date")]
        [Required(ErrorMessage = "Order date is required")]
        [DataType(DataType.Date)]
        public DateTime OrderDate { get; set; } = DateTime.Today;

        [Required(ErrorMessage = "Sample name is required")]
        [StringLength(100)]
        [Display(Name = "Sample Name")]
        public string SampleName { get; set; }

        [Required(ErrorMessage = "Number of samples is required")]
        [Range(1, 1000, ErrorMessage = "Number of samples must be between 1 and 1000")]
        [Display(Name = "Number of Samples")]
        public int NumberOfSamples { get; set; }

        [Display(Name = "Manufacture Date")]
        [DataType(DataType.Date)]
        public DateTime? SampleManufactureDate { get; set; }

        [Display(Name = "Expiration Date")]
        [DataType(DataType.Date)]
        public DateTime? SampleExpirationDate { get; set; }

        [Display(Name = "Analyses")]
        [Required(ErrorMessage = "Please select at least one analysis")]
        public List<int> SelectedAnalysisIds { get; set; } = new();
    }
}
