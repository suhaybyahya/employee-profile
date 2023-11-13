using AutoMapper;
using Employee_Profile.Managers.Interfaces;
using Employee_Profile.Models;
using Employee_Profile.Repository.Interfaces;
using Employee_Profile.ViewModel;

namespace Employee_Profile.Managers
{
    public class EmployeesManager : IEmployeesManager
    {
        IEmployeesRepository _employeesRepository;
        private readonly IMapper _mapper;
        private readonly Logger.ILogger _logger;


        public EmployeesManager(IEmployeesRepository employeesRepository, IMapper mapper, Logger.ILogger logger)
        {
            _employeesRepository = employeesRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Employee> Add(EmployeeViewModel employeeViewModel)
        {
            _logger.LogDebug("EmployeesManager.Add");
            Employee employee = _mapper.Map<Employee>(employeeViewModel);
            employee.CreatedAt = DateTime.Now;
            return await _employeesRepository.Add(employee);
        }

        public Task<IEnumerable<Employee>> GetAllAsync()
        {
            _logger.LogDebug("EmployeesManager.GetAllAsync");
            return _employeesRepository.GetAllAsync();
        }

        public Task<IEnumerable<Employee>> GetByDepartmentId(int departmentId)
        {
            _logger.LogDebug($"EmployeesManager.GetByDepartmentId {departmentId}");
            return _employeesRepository.GetByDepartmentId(departmentId);
        }

        public Task<Employee?> GetByIdAsync(int id)
        {
            _logger.LogDebug($"EmployeesManager.GetByIdAsync : {id}");
            return _employeesRepository.GetByIdAsync(id);
        }

        public async Task<Employee?> Update(int id, EmployeeViewModel employeeViewModel)
        {
            _logger.LogDebug($"EmployeesManager.Update : {id}");
            Employee? employee = await this.GetByIdAsync(id);
            if (employee == null)
                return null;

            employee.Name = employeeViewModel.Name;
            employee.DateOfBirth = employeeViewModel.DateOfBirth;
            employee.Phone = employeeViewModel.Phone;
            employee.Salary = employeeViewModel.Salary;
            employee.DepartmentID = employeeViewModel.DepartmentID;
            employee.UpdatedAt = DateTime.Now;

            await _employeesRepository.SaveChanges();
            return employee;
        }

        public async Task<bool> Delete(int id)
        {
            _logger.LogDebug($"EmployeesManager.Delete : {id}");
            return await _employeesRepository.Delete(id) > 0;
        }
    }
}
