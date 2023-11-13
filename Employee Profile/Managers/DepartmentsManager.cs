using Employee_Profile.Managers.Interfaces;
using Employee_Profile.Models;
using Employee_Profile.Repository.Interfaces;
using Employee_Profile.ViewModel;

namespace Employee_Profile.Managers
{
    public class DepartmentsManager : IDepartmentsManager
    {
        private IDepartmentsRepository _departmentsRepository;
        private IEmployeesRepository _employeesRepository;
        public DepartmentsManager(IDepartmentsRepository departmentsRepository, IEmployeesRepository employeesRepository)
        {
            _departmentsRepository = departmentsRepository;
            _employeesRepository = employeesRepository;
        }

        public Task ApplyRaise(RaiseType raiseType, float value, int departmentId)
        {
            if (raiseType == RaiseType.Fixed)
                _employeesRepository.ApplyFixedRaise(departmentId, value);

            if (raiseType == RaiseType.Percentage)
                _employeesRepository.ApplyPercentageRaise(departmentId, value);

            return Task.CompletedTask;
        }

        public Task<IEnumerable<Department>> GetAllAsync()
        {
            return _departmentsRepository.GetAllAsync();
        }

        public Task<Department?> GetDepartmentById(int id)
        {
            return _departmentsRepository.GetDepartmentById(id);
        }
    }
}
