using EmployeeProject.DTO;
using EmployeeProject.Models;
using EmployeeProject.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeProject.Controllers
{
	[Route("api/admin")]
	[ApiController]
	public class AdminController : ControllerBase
	{
        /// <summary>
        /// Defines the AdminService.
        /// </summary>
        private readonly IAdminService fetchadminService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AdminController"/> class.
        /// </summary>
        /// <param name="fetchadminService">fetchadminService<see cref="IAdminService"/>.</param>
        public AdminController(IAdminService fetchadminService)
        {

            this.fetchadminService = fetchadminService;
        }

        /// <summary>
        /// Add Employee
        /// </summary>
        /// <returns></returns>
     
        [HttpPost("addEmployee")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddEmployee([FromBody] Employee empRequest)
        {
           
            var results = await fetchadminService.AddEmployeeAsync(empRequest);
            return Ok(results);
         
        }

        /// <summary>
        /// Add Employee
        /// </summary>
        /// <returns></returns>
       
        [HttpPut("updateEmployee")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateEmployee(int id, EmployeeDto emp)
        {

            var results = await fetchadminService.UpdateEmployeeAsync(id,emp);
            return Ok(results);

        }

        /// <summary>
        /// Add Employee
        /// </summary>
        /// <returns></returns>

       
        [HttpDelete("deleteEmployee")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteEmployee(int empid)
        {

            var results = await fetchadminService.DeleteEmployeeAsync(empid);
            return Ok(results);

        }
    }
}
