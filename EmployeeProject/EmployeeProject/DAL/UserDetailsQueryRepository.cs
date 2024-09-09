using EmployeeProject.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeProject.DAL
{
	public class UserDetailsQueryRepository: IUserDetailsQueryRepository
	{
       
        private readonly IDbContextFactory<EmployeeQueryDbContext> contextFactory;


        public UserDetailsQueryRepository(
            IDbContextFactory<EmployeeQueryDbContext> contextFactory)
        {
           
            this.contextFactory = contextFactory;

        }

        /// <summary>
        /// FetchEmployeeDetails.
        /// </summary>
        /// <param name="EmpId">EmpId.</param>
        /// <returns>Employee.</returns>
        public async Task<Employee> GetEmployeeDetails(int EmpId)
        {
            using (var context = this.contextFactory.CreateDbContext())
            {
                var result = await context.Employee
                                .AsNoTracking()
                                .SingleOrDefaultAsync(x => x.EmployeeId == EmpId);
                return result;
            }
        }
    }
}
