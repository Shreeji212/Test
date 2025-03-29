using System.ComponentModel.DataAnnotations;

namespace ConsumeWebAPI.Models
{
    public class EmployeeViewModel
    {
        public int EmployeeId { get; set; }

        [Required]
        public string? FirstName { get; set; }

        public string LastName { get; set; } = null!;

        [Range(int.MinValue, int.MaxValue, ErrorMessage = "Salary for {0} must be an integer.")]
        public int? Salary { get; set; }

        [Required]
        public DateTime JoiningDate { get; set; }

        public int? ReportingPerson { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        public bool IsDeleted { get; set; }

        [Required(ErrorMessage = "Designation is required.")]
        public string? DepartmentName { get; set; }

        [Required(ErrorMessage = "Department is required.")]
        public string? DesignationName { get; set; }

        public string? KnowledgeName { get; set; }
    }
}
