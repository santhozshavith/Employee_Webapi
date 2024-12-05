using Employee_Webapi.IService;
using Employee_Webapi.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Employee_Webapi.Controllers
{
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpPost("Save")]
        public async Task<IActionResult> Save([FromBody] Employee employee)
        {
           
            try
            {
                var employeeId = await _employeeService.Save(employee);  
                return Ok(new { EmployeeId = employeeId, Message = "Employee saved successfully!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");  
            }
        }

        [HttpPost("View")]
        public async Task<IActionResult> View(String Id)
        {

            try
            {
                var employeeId = await _employeeService.View(Id);
                return Ok(new { EmployeeId = employeeId, Message = "Employee saved successfully!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("Update")]
        public async Task<IActionResult> Update([FromBody] Employee employee)
        {

            try
            {
                var employeeId = await _employeeService.Update(employee);
                return Ok(new { EmployeeId = employeeId, Message = "Employee saved successfully!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("Delete")]
        public async Task<IActionResult> Delete([FromBody] string Id)
        {

            try
            {
                var employeeId = await _employeeService.Delete(Id);
                return Ok(new { EmployeeId = employeeId, Message = "Employee Deleted successfully!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("GetEmployeeList")]
        public async Task<IActionResult> Get()
        {

            try
            {
                var employeeId = await _employeeService.GetEmployeeList();
                return Ok(new { EmployeeId = employeeId, Message = "Employee saved successfully!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


    }
}
