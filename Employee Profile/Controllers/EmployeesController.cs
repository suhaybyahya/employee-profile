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

        public EmployeesController(IEmployeesManager employeesManager, IDepartmentsManager departmentsManager)
        {
            _employeesManager = employeesManager;
            _departmentsManager = departmentsManager;
        }

        // GET: api/<EmployeesController>
        [HttpGet]
        public IEnumerable<Employee> Get()
        {
            return _employeesManager.GetAllAsync().Result;
        }

        [HttpGet]
        [Route("GetDepartmentEmployees")]
        public IEnumerable<Employee> GetDepartmentEmployees(int departmentId)
        {
            return _employeesManager.GetByDepartmentId(departmentId).Result;
        }

        // GET api/<EmployeesController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> Get(int id)
        {
            var employee = await _employeesManager.GetByIdAsync(id);
            if (employee == null)
                return BadRequest("Employee id doesn't exist");

            return Ok(employee);

        }

        // POST api/<EmployeesController>
        [HttpPost]
        public async Task<ActionResult<Employee>> Post([FromBody] EmployeeViewModel employee)
        {
            try
            {
                if (!ModelState.IsValid || employee == null)
                    return BadRequest();

                var department = await _departmentsManager.GetDepartmentById(employee.DepartmentID);

                if (department == null)
                    return BadRequest("Department specified is not availlable");

                var createdEmployee = await _employeesManager.Add(employee);

                return CreatedAtAction("Post",
                    new { id = createdEmployee.Id }, createdEmployee);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating new employee record");
            }

        }

        // PUT api/<EmployeesController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Employee>> Put(int id, [FromBody] EmployeeViewModel employeeViewModel)
        {
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

        // DELETE api/<EmployeesController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            if (await _employeesManager.Delete(id))
                return Ok();
            return BadRequest("Employee specified is not availlable");
        }
    }
}
