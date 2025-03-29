using Employee.Data.Entities;
using Employee.Data.IRepositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Employee.Data.Repositories
{
    public class EmployeeRepository : GenericRepository<Entities.Employee>, IEmployeeRepository
    {
        #region Properties
        protected readonly EmployeeManagementContext _context;
        #endregion

        #region Constructor
        public EmployeeRepository(EmployeeManagementContext context) : base(context)
        {
            _context = context;
        }
        #endregion

        #region Public Methods
        public async Task<List<Entities.Employee>> GetAllEmployees(string? condition, string sortColumn, string sortDirection, int startIndex, int pageSize)
        {
            string sp = "EXEC [dbo].[GetAllEmployees] @CONDITIONS, @SORT_COLUMN, @SORT_DIRECTION, @START_INDEX, @PAGE_SIZE";
            List<SqlParameter> parameters = new()
            {
                new SqlParameter{ ParameterName= "@CONDITIONS", Value = string.IsNullOrEmpty(condition) ? "" : condition},
                new SqlParameter{ ParameterName= "@SORT_COLUMN", Value = sortColumn},
                new SqlParameter{ ParameterName= "@SORT_DIRECTION", Value = sortDirection},
                new SqlParameter{ ParameterName= "@START_INDEX", Value = startIndex},
                new SqlParameter{ ParameterName= "@PAGE_SIZE", Value = pageSize}
            };
            return await _context.Employees.FromSqlRaw(sp, parameters.ToArray()).ToListAsync();
            //return await _context.Employees.Where(x => !x.IsDeleted).ToListAsync();
        }

        public async Task<List<Entities.Employee>> GetEmployees(DateTime? From, DateTime? To, string? Name, int? DesignationId)
        {
            //string sp = "EXEC [dbo].[GetAllEmployees_BySearch] @Name @Fromdate @Todate";
            string sp = "EXEC [dbo].[GetAllEmployees_BySearch] @Name";
            List<SqlParameter> parameters = new()
            {
                new SqlParameter{ ParameterName= "@Name", Value = string.IsNullOrEmpty(Name) ? "" : Name}
                //new SqlParameter{ ParameterName= "@Fromdate", Value = From},
                //new SqlParameter{ ParameterName= "@Todate", Value = To}
            };
            return await _context.Employees.FromSqlRaw(sp, parameters.ToArray()).ToListAsync();
        }

        public async Task<List<Entities.Department>> ListDepartment()
        {
            string sp = "EXEC [dbo].[GetDepartment]";
            return await _context.Departments.FromSqlRaw(sp).ToListAsync();
        }

        public async Task<List<Entities.Designation>> ListDesignation()
        {
            string sp = "EXEC [dbo].[GetDesignation]";
            return await _context.Designations.FromSqlRaw(sp).ToListAsync();
        }

        public async Task<List<Entities.Knowledge>> ListKnowledges()
        {
            string sp = "EXEC [dbo].[GetKnowledge]";
            return await _context.Knowledges.FromSqlRaw(sp).ToListAsync();
        }
        #endregion
    }
}
