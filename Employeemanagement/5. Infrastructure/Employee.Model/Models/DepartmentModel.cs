namespace Employee.Model.Models
{
    public class DepartmentModel
    {
        public int DepartmentId { get; set; }

        public string DepartmentName { get; set; } = null!;

        public virtual ICollection<EmployeeModel> Employees { get; set; } = new List<EmployeeModel>();
    }
}
