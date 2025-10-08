using Microsoft.AspNetCore.Mvc;
using EmployeeApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace EmployeeApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {

        private static readonly List<Employee> Employees = new List<Employee>
        {
            new Employee { Id = 1, Name = "Amit", Department = "HR", MobileNo = "9876543210", Email = "amit@gmail.com" },
            new Employee { Id = 2, Name = "Sagar", Department = "eee", MobileNo = "7777778888", Email = "sagar@encora.com" },
             new Employee { Id = 3, Name = "Singh", Department = "IT", MobileNo = "8888888888", Email = "priya@yahoo.com" }
        };

     
        [HttpGet]
        public ActionResult<IEnumerable<Employee>> GetAllEmployees()
        {
            return Ok(Employees);
        }

        [HttpGet("{id}")]
        public ActionResult<Employee> GetEmployeeById([FromRoute] int id)
        {
            var emp = Employees.FirstOrDefault(e => e.Id == id);
            if (emp == null) return NotFound();
            return Ok(emp);
        }

       
        [HttpGet("bydept")]
        public ActionResult<IEnumerable<Employee>> GetEmployeesByDept([FromQuery] string department)
        {
            if (string.IsNullOrWhiteSpace(department))
                return BadRequest("Department query parameter is required.");

            var list = Employees.Where(e => string.Equals(e.Department, department, System.StringComparison.OrdinalIgnoreCase)).ToList();
            return Ok(list);
        }

        
        [HttpPost]
        public ActionResult<Employee> AddEmployee([FromBody] Employee employee)
        {
            
            if (Employees.Any(e => e.Id == employee.Id))
            {
                return Conflict($"Employee with Id {employee.Id} already exists.");
            }

            Employees.Add(employee);
            
            return CreatedAtAction(nameof(GetEmployeeById), new { id = employee.Id }, employee);
        }

        
        [HttpPut("{id}")]
        public IActionResult UpdateEmployee([FromRoute] int id, [FromBody] Employee employee)
        {
            if (id != employee.Id)
                return BadRequest("Id in route and body must match.");

            var existing = Employees.FirstOrDefault(e => e.Id == id);
            if (existing == null) return NotFound();

            
            existing.Name = employee.Name;
            existing.Department = employee.Department;
            existing.MobileNo = employee.MobileNo;
            existing.Email = employee.Email;

            return NoContent(); 
        }

        
        [HttpPatch("{id}/email")]
        public IActionResult UpdateEmployeeEmail([FromRoute] int id, [FromBody] EmailUpdateDto dto)
        {
            var existing = Employees.FirstOrDefault(e => e.Id == id);
            if (existing == null) return NotFound();

            existing.Email = dto.Email;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteEmployee([FromRoute] int id)
        {
            var existing = Employees.FirstOrDefault(e => e.Id == id);
            if (existing == null) return NotFound();

            Employees.Remove(existing);
            return NoContent();
        }

       
        [HttpHead("{id}")]
        public IActionResult HeadEmployee([FromRoute] int id)
        {
            var exists = Employees.Any(e => e.Id == id);
            if (!exists) return NotFound();
            return Ok(); 
        }

        [HttpOptions]
        public IActionResult Options()
        {
            
            Response.Headers.Add("Allow", "GET,POST,PUT,PATCH,DELETE,HEAD,OPTIONS");
            return Ok();
        }
    }
}
