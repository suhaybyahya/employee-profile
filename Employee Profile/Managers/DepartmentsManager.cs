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
        private Logger.ILogger _logger;
        public DepartmentsManager(IDepartmentsRepository departmentsRepository, IEmployeesRepository employeesRepository, Logger.ILogger logger)
        {
            _departmentsRepository = departmentsRepository;
            _employeesRepository = employeesRepository;
            _logger = logger;
        }

        public Task ApplyRaise(RaiseType raiseType, float value, int departmentId)
        {
            _logger.LogDebug($"DepartmentsManager.ApplyRaise params : {raiseType} {value} {departmentId}");

            if (raiseType == RaiseType.Fixed)
                _employeesRepository.ApplyFixedRaise(departmentId, value);

            if (raiseType == RaiseType.Percentage)
                _employeesRepository.ApplyPercentageRaise(departmentId, value);

            return Task.CompletedTask;
        }

        public Task<IEnumerable<Department>> GetAllAsync()
        {
            _logger.LogDebug($"DepartmentsManager.GetAllAsync");
            return _departmentsRepository.GetAllAsync();
        }

        public Task<Department?> GetDepartmentById(int id)
        {
            _logger.LogDebug($"DepartmentsManager.GetAllAsync params : {id}");
            return _departmentsRepository.GetDepartmentById(id);
        }
    }
}
