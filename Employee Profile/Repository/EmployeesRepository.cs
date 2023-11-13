using Employee_Profile.Models;
using Employee_Profile.Repository.Context;
using Employee_Profile.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Employee_Profile.Repository
{
    public class EmployeesRepository : IEmployeesRepository
    {
        private readonly EmployeeProfileContext _employeeProfileContext;
        public EmployeesRepository(EmployeeProfileContext context) {
            _employeeProfileContext = context ??
           throw new ArgumentNullException(nameof(context));
        }

        public async Task<Employee> Add(Employee employee)
        {
            await _employeeProfileContext.Employees.AddAsync(employee);
            await _employeeProfileContext.SaveChangesAsync();
            return employee;
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await _employeeProfileContext.Employees.ToListAsync();
        }

        public async Task<IEnumerable<Employee>> GetByDepartmentId(int departmentId)
        {
            return await _employeeProfileContext.Employees.Where(a => a.DepartmentID == departmentId).ToListAsync();
        }

        public async Task<Employee?> GetByIdAsync(int id)
        {
            return await _employeeProfileContext.Employees.FindAsync(id);
        }

        public async Task SaveChanges()
        {
            await _employeeProfileContext.SaveChangesAsync();
        }

        public async Task<int> Delete(int id)
        {
            return await _employeeProfileContext.Employees.Where(e => e.Id == id).ExecuteDeleteAsync();
        }
        public bool ApplyFixedRaise(int departmentId, float value)
        {
            _employeeProfileContext.Employees
              .Where(e => e.DepartmentID == departmentId)
              .ToList()
              .ForEach(a => a.Salary = a.Salary + value);

            return _employeeProfileContext.SaveChanges() > 0;

        }

        public bool ApplyPercentageRaise(int departmentId, float value)
        {
            _employeeProfileContext.Employees
              .Where(e => e.DepartmentID == departmentId)
              .ToList()
              .ForEach(a => a.Salary = a.Salary + (a.Salary * value));

            return _employeeProfileContext.SaveChanges() > 0;

        }

    }
}
