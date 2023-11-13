using Employee_Profile.Models;

namespace Employee_Profile.Repository.Interfaces
{
    public interface IDepartmentsRepository
    {
        public Task<IEnumerable<Department>> GetAllAsync();
        public Task<Department?> GetDepartmentById(int id);

    }
}
