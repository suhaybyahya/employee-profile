using Employee_Profile.Models;
using Employee_Profile.ViewModel;

namespace Employee_Profile.Managers.Interfaces
{
    public interface IDepartmentsManager
    {
        public Task<IEnumerable<Department>> GetAllAsync();
        public Task<Department?> GetDepartmentById(int id);
        public Task ApplyRaise(RaiseType raiseType,float value, int departmentId);
    }
}
