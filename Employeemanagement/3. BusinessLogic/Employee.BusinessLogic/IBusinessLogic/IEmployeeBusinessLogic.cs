using Employee.Model.Models;
using Employee.Model.RequestModels;

namespace Employee.BusinessLogic.IBusinessLogic
{
    public interface IEmployeeBusinessLogic
    {
        Task<ResponseModel> GetAllEmployees(string? condition, string sortColumn, string sortDirection, int startIndex, int pageSize);
        Task<ResponseModel> GetEmployees(DateTime? From, DateTime? To, string Name, int? DesignationId);
        Task<ResponseModel> AddEmployee(EmployeeRequestModel employee);
        Task<ResponseModel> UpdateEmployee(EmployeeRequestModel employeeRequestModel);
        Task<ResponseModel> GetEmployeeById(int employeeId);
        Task<ResponseModel> DeleteEmployeeById(int employeeId);
        Task<ResponseModel> GetDepartment();
        Task<ResponseModel> GetDesignation();
        Task<ResponseModel> GetKnowledge();
    }
}
