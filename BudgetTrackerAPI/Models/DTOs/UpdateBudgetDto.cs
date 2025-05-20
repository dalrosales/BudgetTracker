using System.ComponentModel.DataAnnotations;

namespace BudgetTrackerAPI.Models.DTOs
{
    public class UpdateBudgetDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Budgeted amount must be a non-negative number.")]
        public decimal BudgetedAmount { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Actual amount must be a non-negative number.")]
        public decimal ActualAmount { get; set; }

        [Required]
        [MaxLength(50)]
        public string Period { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
