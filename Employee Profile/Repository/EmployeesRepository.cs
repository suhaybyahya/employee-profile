using Employee_Profile.Models;
using Employee_Profile.Repository.Context;
using Employee_Profile.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Employee_Profile.Repository
{
    public class EmployeesRepository : IEmployeesRepository
    {
        private readonly EmployeeProfileContext _employeeProfileContext;
        private readonly Logger.ILogger _logger;
        public EmployeesRepository(EmployeeProfileContext context, Logger.ILogger logger)
        {
            _employeeProfileContext = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger;
        }

        public async Task<Employee> Add(Employee employee)
        {
            _logger.LogDebug("EmployeesRepository.Add");
            await _employeeProfileContext.Employees.AddAsync(employee);
            await _employeeProfileContext.SaveChangesAsync();
            return employee;
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            _logger.LogDebug("EmployeesRepository.GetAllAsync");
            return await _employeeProfileContext.Employees.ToListAsync();
        }

        public async Task<IEnumerable<Employee>> GetByDepartmentId(int departmentId)
        {
            _logger.LogDebug($"EmployeesRepository.GetByDepartmentId : {departmentId}");
            return await _employeeProfileContext.Employees.Where(a => a.DepartmentID == departmentId).ToListAsync();
        }

        public async Task<Employee?> GetByIdAsync(int id)
        {
            _logger.LogDebug($"EmployeesRepository.GetByIdAsync : {id}");
            return await _employeeProfileContext.Employees.FindAsync(id);
        }

        public async Task SaveChanges()
        {
            await _employeeProfileContext.SaveChangesAsync();
        }

        public async Task<int> Delete(int id)
        {
            _logger.LogDebug($"EmployeesRepository.Delete : {id}");
            return await _employeeProfileContext.Employees.Where(e => e.Id == id).ExecuteDeleteAsync();
        }
        public bool ApplyFixedRaise(int departmentId, float value)
        {
            _logger.LogDebug($"EmployeesRepository.ApplyFixedRaise : {departmentId} {value}");
            _employeeProfileContext.Employees
              .Where(e => e.DepartmentID == departmentId)
              .ToList()
              .ForEach(a => a.Salary = a.Salary + value);

            return _employeeProfileContext.SaveChanges() > 0;

        }

        public bool ApplyPercentageRaise(int departmentId, float value)
        {
            _logger.LogDebug($"EmployeesRepository.ApplyPercentageRaise : {departmentId} {value}");
            _employeeProfileContext.Employees
              .Where(e => e.DepartmentID == departmentId)
              .ToList()
              .ForEach(a => a.Salary = a.Salary + (a.Salary * value));

            return _employeeProfileContext.SaveChanges() > 0;

        }

    }
}
