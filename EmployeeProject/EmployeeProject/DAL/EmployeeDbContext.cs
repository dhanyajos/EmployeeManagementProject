using EmployeeProject.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeProject.DAL
{
	public class EmployeeDbContext : DbContext
	{
        protected EmployeeDbContext(DbContextOptions<EmployeeDbContext> options)
         : base(options)
        {
        }

        protected EmployeeDbContext(DbContextOptions options)
          : base(options)
        {
        }

        public virtual DbSet<Employee> Employee { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable(name: "Employee", schema: "EMP");
                entity.HasIndex(e => e.Email).IsUnique();

            });
        }
    }
}
