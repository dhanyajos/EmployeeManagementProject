using EmployeeProject.DTO;
using EmployeeProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace EmployeeProject.DAL
{
    public class EmployeeDetailsCommandRepository : IEmployeeDetailsCommandRepository
    {
        private readonly IDbContextFactory<EmployeeCommandDbContext> contextFactory;

        public EmployeeDetailsCommandRepository(
            IDbContextFactory<EmployeeCommandDbContext> contextFactory)
        {

            this.contextFactory = contextFactory;

        }
        public async Task<Employee> SaveEmployeeDetails(Employee emp)
        {
            using (var context = this.contextFactory.CreateDbContext())
            {
                try
                {

                    var empresult = await context.Employee.FirstOrDefaultAsync(x => x.Email == emp.Email);
                    if (empresult != null)
                    {
                        return null;
                    }
                    else
                    {
                        await context.Employee.AddAsync(emp);
                        if (await this.CommitChangesAsync(context))
                        {
                            return emp;
                        }
                    }
                }
                catch (Exception ex)
                {

                    return null;
                }

                return null;
            }
        }

        private async Task<bool> CommitChangesAsync(EmployeeCommandDbContext context)
        {


            try
            {
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }


        public async Task<bool> UpdateEmployeeDetails(int id, EmployeeDto emp)
        {

            bool returnValue = false;
            using (var context = this.contextFactory.CreateDbContext())
            {

                var empInstance = await context.Employee
                                    .FindAsync(id);
                try
                {
                    if (empInstance != null)
                    {
                        empInstance.FirstName = emp.FirstName;
                        empInstance.LastName =  emp.LastName;
                        empInstance.Email =  emp.Email;
                        empInstance.DateOfBirth =  empInstance.DateOfBirth;
                        empInstance.Department = emp.Department;
                        empInstance.Salary = emp.Salary;

                        context.Update(empInstance);
                    }
                }
                finally
                {
                    if (empInstance != null)
                    {
                        returnValue = await this.CommitChangesAsync(context);
                    }
                }

                return returnValue;
            }

        }

        public async Task<bool> DeleteEmployeeDetails(int empid)
        {

            bool returnValue = false;
            using (var context = this.contextFactory.CreateDbContext())
            {
                var empres = await context.Employee.FindAsync(empid);
                if (empres != null)
                {
                    context.Employee.Remove(empres);
                    await context.SaveChangesAsync();
                    returnValue = true;

                }
                return returnValue;
            }
        }
    }
}
