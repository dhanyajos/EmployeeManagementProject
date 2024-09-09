using Microsoft.EntityFrameworkCore;

namespace EmployeeProject.DAL
{
	public class EmployeeCommandDbContext : EmployeeDbContext
	{
        internal EmployeeCommandDbContext(DbContextOptions options)
     : base(options)
        {
        }

        public EmployeeCommandDbContext(DbContextOptions<EmployeeCommandDbContext> options)
        : base(options)
        {
        }
    }
}
