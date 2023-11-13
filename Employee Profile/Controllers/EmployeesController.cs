using Employee_Profile.Managers.Interfaces;
using Employee_Profile.Models;
using Employee_Profile.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Employee_Profile.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private IEmployeesManager _employeesManager;
        private IDepartmentsManager _departmentsManager;
        private Logger.ILogger _logger;

        public EmployeesController(IEmployeesManager employeesManager, IDepartmentsManager departmentsManager, Logger.ILogger logger)
        {
            _employeesManager = employeesManager;
            _departmentsManager = departmentsManager;
            _logger = logger;
        }

        // GET: api/<EmployeesController>
        [HttpGet]
        public IEnumerable<Employee> Get()
        {
            try
            {
                _logger.LogDebug("EmployeesController.Get");
                return _employeesManager.GetAllAsync().Result;
            }
            catch (Exception e)
            {
                _logger.LogError($"Error while retrieving employees : {e.Message} , {e.StackTrace}");
                return Enumerable.Empty<Employee>();
            }
        }

        [HttpGet]
        [Route("GetDepartmentEmployees")]
        public IEnumerable<Employee> GetDepartmentEmployees(int departmentId)
        {
            try
            {
                _logger.LogDebug($"EmployeesController.GetDepartmentEmployees params : {departmentId}");
                return _employeesManager.GetByDepartmentId(departmentId).Result;
            }
            catch (Exception e)
            {
                _logger.LogError($"Error while accessing EmployeesController.GetDepartmentEmployees : {e.Message} , {e.StackTrace}");
                return Enumerable.Empty<Employee>();
            }
        }

        // GET api/<EmployeesController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> Get(int id)
        {
            try
            {
                _logger.LogDebug($"EmployeesController.GetById params : {id}");
                var employee = await _employeesManager.GetByIdAsync(id);
                if (employee == null)
                    return BadRequest("Employee id doesn't exist");

                return Ok(employee);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error while accessing EmployeesController.GetById : {e.Message} , {e.StackTrace}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error while executing EmployeesController.GetById API");
            }
        }

        // POST api/<EmployeesController>
        [HttpPost]
        public async Task<ActionResult<Employee>> Post([FromBody] EmployeeViewModel employeeViewModel)
        {
            try
            {
                _logger.LogDebug($"EmployeesController.Post params : {employeeViewModel.Name} , {employeeViewModel.Phone} ," +
                    $" {employeeViewModel.Salary} , {employeeViewModel.DepartmentID} , {employeeViewModel.DateOfBirth}");

                if (!ModelState.IsValid || employeeViewModel == null)
                    return BadRequest();

                var department = await _departmentsManager.GetDepartmentById(employeeViewModel.DepartmentID);

                if (department == null)
                    return BadRequest("Department specified is not availlable");

                var createdEmployee = await _employeesManager.Add(employeeViewModel);

                return CreatedAtAction("Post",
                    new { id = createdEmployee.Id }, createdEmployee);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error while accessing EmployeesController.GetById : {e.Message} , {e.StackTrace}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating new employee record");
            }

        }

        // PUT api/<EmployeesController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Employee>> Put(int id, [FromBody] EmployeeViewModel employeeViewModel)
        {
            try
            {
                _logger.LogDebug($"EmployeesController.Put params : {employeeViewModel.Name} , {employeeViewModel.Phone} , {employeeViewModel.Salary}" +
                    $" , {employeeViewModel.DepartmentID} , {employeeViewModel.DateOfBirth}");

                if (!ModelState.IsValid || employeeViewModel == null)
                    return BadRequest();

                var department = await _departmentsManager.GetDepartmentById(employeeViewModel.DepartmentID);

                if (department == null)
                    return BadRequest("Department specified is not availlable");

                var employee = await _employeesManager.Update(id, employeeViewModel);
                if (employee == null)
                    return BadRequest("Employee specified is not availlable");
                return Ok(employee);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error while accessing EmployeesController.Put : {e.Message} , {e.StackTrace}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while trying to update record");
            }
        }

        // DELETE api/<EmployeesController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                _logger.LogDebug($"EmployeesController.Put params : {id}");
                if (await _employeesManager.Delete(id))
                    return Ok();
                return BadRequest("Employee specified is not availlable");
            }
            catch (Exception e)
            {
                _logger.LogError($"Error while accessing EmployeesController.Delete : {e.Message} , {e.StackTrace}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while trying to update record");
            }
        }
    }
}
