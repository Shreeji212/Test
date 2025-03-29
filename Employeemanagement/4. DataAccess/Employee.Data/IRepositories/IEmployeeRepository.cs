namespace Employee.Data.IRepositories
{
    public interface IEmployeeRepository : IGenericRepository<Entities.Employee>
    {
        Task<List<Entities.Employee>> GetAllEmployees(string? condition, string sortColumn, string sortDirection, int startIndex, int pageSize);
        Task<List<Entities.Employee>> GetEmployees(DateTime? From, DateTime? To, string Name, int? DesignationId);
        Task<List<Entities.Designation>> ListDesignation();
        Task<List<Entities.Department>> ListDepartment();
        Task<List<Entities.Knowledge>> ListKnowledges();
    }
}
