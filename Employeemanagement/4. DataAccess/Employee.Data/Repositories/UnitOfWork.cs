using Employee.Data.Entities;
using Employee.Data.IRepositories;

namespace Employee.Data.Repositories
{
    public class UnitOfwork : IUnitOfwork, IDisposable
    {
        private readonly EmployeeManagementContext _context;
        private bool disposed = false;

        public UnitOfwork(EmployeeManagementContext context)
        {
            _context = context;
            Employee = new EmployeeRepository(_context);
            Department = new EmployeeRepository(_context);
            Designation = new EmployeeRepository(_context);
            Knowledge = new EmployeeRepository(_context);
        }

        public IEmployeeRepository Employee { get; private set; }

        public IEmployeeRepository Department { get; private set; }

        public IEmployeeRepository Designation { get; private set; }

        public IEmployeeRepository Knowledge { get; private set; }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    // called via myClass.Dispose(). 
                    // OK to use any private object references
                    _context.Dispose();
                }
                // Release unmanaged resources.
                // Set large fields to null.                
                disposed = true;
            }
        }

        public void Dispose() // Implement IDisposable
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~UnitOfwork() // the finalizer
        {
            Dispose(false);
        }
    }
}
