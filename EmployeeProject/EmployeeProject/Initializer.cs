using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using EmployeeProject.DAL;
using EmployeeProject.Services;
using EmployeeProject.Workers;

namespace EmployeeProject
{
    public class Initializer
    {
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Defines the _services.
        /// </summary>
        private readonly IServiceCollection _services;



        /// <summary>
        /// Initializes a new instance of the <see cref="Initializer"/> class.
        /// </summary>
        /// <param name="configuration">The configuration<see cref="IConfiguration"/>.</param>
        /// <param name="services">The services<see cref="IServiceCollection"/>.</param>
        public Initializer(IConfiguration configuration, IServiceCollection services)
        {
            _configuration = configuration;
            _services = services;
          

        }

        /// <summary>
        /// The Initialize.
        /// </summary>
        public void Initialize()
        {
           
            this.InitializeServices();
            this.InitializeRepositories();

            // Adding the DB context to dependency injection profile
            var commandConnectionString = this._configuration.GetConnectionString("CommandConnectionString");
            var queryConnectionString = this._configuration.GetConnectionString("QueryConnectionString");
            
            this._services.AddPooledDbContextFactory<EmployeeCommandDbContext>(options =>
            {
                options.UseSqlServer(commandConnectionString, sqlServerOptions =>
                {
                    sqlServerOptions.CommandTimeout(60);
                    sqlServerOptions.EnableRetryOnFailure();
                });
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
               
            });

            this._services.AddPooledDbContextFactory<EmployeeQueryDbContext>(options =>
            {
                options.UseSqlServer(queryConnectionString, sqlServerOptions =>
                {
                    sqlServerOptions.CommandTimeout(60);
                    sqlServerOptions.EnableRetryOnFailure();
                });
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });



        }
        private void InitializeRepositories()
        {
            _services.AddScoped<IEmployeeDetailsCommandRepository, EmployeeDetailsCommandRepository>();
            _services.AddScoped<IUserDetailsQueryRepository, UserDetailsQueryRepository>();
        }

        private void InitializeServices()
        {
            _services.AddScoped<IAdminService, AdminService>();
            _services.AddScoped<IAdminWorker, AdminWorker>();
            _services.AddScoped<IUserService, UserService>();
            _services.AddScoped<IUserWorker, UserWorker>();

        }
    }
}
