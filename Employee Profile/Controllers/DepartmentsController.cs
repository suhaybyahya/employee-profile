using Employee_Profile.Managers.Interfaces;
using Employee_Profile.Models;
using Employee_Profile.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Employee_Profile.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private IDepartmentsManager _departmentsManager;
        Logger.ILogger _logger;

        public DepartmentsController(IDepartmentsManager departmentsManager, Logger.ILogger logger)
        {
            _logger = logger;
            _departmentsManager = departmentsManager;
        }


        // GET: api/<DepartmentsController>
        [HttpGet]
        public IEnumerable<Department> Get()
        {
            try
            {
                _logger.LogError("Get Departments");
                return _departmentsManager.GetAllAsync().Result;
            }
            catch (Exception e)
            {
                _logger.LogError($"Error while retrieving departments : {e.Message}");
                return Enumerable.Empty<Department>();
            }
        }

        // GET: api/<DepartmentsController>
        [HttpPost]
        [Route("RaiseSalaries")]
        public ActionResult RaiseSalaries(int departmentId, [FromBody] RaiseSalaryViewModel raiseSalaryViewModel)
        {

            try
            {
                RaiseType raiseType;

                if (!Enum.TryParse(raiseSalaryViewModel.RaiseType, out raiseType))
                    return BadRequest("Raise type is not recognized, Should be eathier Fixed/Percentage");

                _departmentsManager.ApplyRaise(raiseType, raiseSalaryViewModel.Value, departmentId);

                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError($"Error while executing RaiseSalaries API : {e.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error while executing RaiseSalaries API");
            }

        }

    }
}
