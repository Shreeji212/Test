using Microsoft.EntityFrameworkCore;

namespace Employee.Data.IRepositories
{
    public interface IUnitOfwork : IDisposable
    {
        IEmployeeRepository Employee { get; }
        IEmployeeRepository Department { get; }
        IEmployeeRepository Designation { get; }
        IEmployeeRepository Knowledge { get; }
        Task<int> CompleteAsync();
        int Complete();
    }
}
