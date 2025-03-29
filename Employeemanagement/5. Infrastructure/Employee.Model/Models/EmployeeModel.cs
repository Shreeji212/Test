namespace Employee.Model.Models
{
    public class EmployeeModel
    {
        public int EmployeeId { get; set; }

        public string? FirstName { get; set; }

        public string LastName { get; set; } = null!;

        public int DesignationId { get; set; }

        public int DepartmentId { get; set; }

        public int? KnowledgeOf { get; set; }

        public int? Salary { get; set; }

        public DateTime JoiningDate { get; set; }

        public int? ReportingPerson { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        public bool IsDeleted { get; set; }

        public string DepartmentName { get; set; } = null!;

        public string DesignationName { get; set; } = null!;

        public string? KnowledgeName { get; set; }

        public virtual DepartmentModel Department { get; set; } = null!;

        public virtual DesignationModel Designation { get; set; } = null!;

        public virtual KnowledgeModel? KnowledgeOfNavigation { get; set; }
    }
}
