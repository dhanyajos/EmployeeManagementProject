using EmployeeProject.Models;
using EmployeeProject.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeProject.Controllers
{
	[Route("api/User")]
	[ApiController]
	public class UserController : ControllerBase
	{
        /// <summary>
        /// Defines the UserService.
        /// </summary>
        private readonly IUserService fetchuserService;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserController"/> class.
        /// </summary>
        /// <param name="fetchuserService">fetchadminService<see cref="IUserService"/>.</param>
        public UserController(IUserService fetchuserService)
        {

            this.fetchuserService = fetchuserService;
        }

        /// <summary>
        /// Add Employee
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetEmployee")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> GetEmployee(int empId)
        {

            var results = await fetchuserService.GetEmployeeAsync(empId);
            return Ok(results);

        }
    }
}
