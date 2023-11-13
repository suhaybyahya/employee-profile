using Employee_Profile.Models;
using Employee_Profile.ViewModel;

namespace Employee_Profile.Managers.Interfaces
{
    public interface IEmployeesManager
    {
        public Task<IEnumerable<Employee>> GetAllAsync();
        public Task<Employee?> GetByIdAsync(int id);
        public Task<IEnumerable<Employee>> GetByDepartmentId(int departmentId);
        public Task<Employee> Add(EmployeeViewModel employeeViewModel);
        public Task<Employee?> Update(int id, EmployeeViewModel employeeViewModel);
        public Task<bool> Delete(int id);

    }
}