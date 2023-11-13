using Employee_Profile.Models;
using Employee_Profile.Repository.Context;
using Employee_Profile.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Employee_Profile.Repository
{
    public class DepartmentsRepository : IDepartmentsRepository
    {
        private readonly EmployeeProfileContext _employeeProfileContext;
        private readonly Logger.ILogger _logger;

        public DepartmentsRepository(EmployeeProfileContext context, Logger.ILogger logger)
        {
            _employeeProfileContext = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger;
        }

        public async Task<IEnumerable<Department>> GetAllAsync()
        {
            _logger.LogDebug($"DepartmentsRepository.GetAllAsync");
            return await _employeeProfileContext.Departments.ToListAsync();
        }

        public async Task<Department?> GetDepartmentById(int id)
        {
            _logger.LogDebug($"DepartmentsRepository.GetDepartmentById params : {id}");
            return await _employeeProfileContext.Departments.FindAsync(id);
        }
    }
}
