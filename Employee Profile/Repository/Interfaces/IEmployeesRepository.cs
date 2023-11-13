using Employee_Profile.Models;

namespace Employee_Profile.Repository.Interfaces
{
    public interface IEmployeesRepository
    {
        public Task<IEnumerable<Employee>> GetAllAsync();
        public Task<Employee?> GetByIdAsync(int id);
        public Task<IEnumerable<Employee>> GetByDepartmentId(int departmentId);
        public Task SaveChanges();
        public Task<Employee> Add(Employee e);
        public Task<int> Delete(int id);
        public bool ApplyFixedRaise(int departmentId, float value);
        public bool ApplyPercentageRaise(int departmentId, float value);
    }
}