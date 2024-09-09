using EmployeeProject.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeProject.DAL
{
	public class EmployeeQueryDbContext : EmployeeDbContext
	{
        internal EmployeeQueryDbContext(DbContextOptions options)
     : base(options)
        {
        }

        public EmployeeQueryDbContext(DbContextOptions<EmployeeQueryDbContext> options)
        : base(options)
        {
        }

        }
    }
