using Employee_Profile.Models;
using Employee_Profile.Repository.Context;
using Employee_Profile.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Employee_Profile.Repository
{
    public class DepartmentsRepository : IDepartmentsRepository
    {
        private readonly EmployeeProfileContext _employeeProfileContext;
        public DepartmentsRepository(EmployeeProfileContext context)
        {
            _employeeProfileContext = context ??
           throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Department>> GetAllAsync()
        {
            return await _employeeProfileContext.Departments.ToListAsync();
        }

        public async Task<Department?> GetDepartmentById(int id)
        {
            return await _employeeProfileContext.Departments.FindAsync(id);
        }
    }
}
