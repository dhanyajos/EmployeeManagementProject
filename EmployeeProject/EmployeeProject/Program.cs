using EmployeeProject;
using EmployeeProject.Authorization;
using EmployeeProject.Services;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Security.Claims;
using System.Text;

public class Program
{
    /// <summary>
    /// Main method - Entry point of the application.
    /// </summary>
    /// <param name="args">The command-line arguments.</param>
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Register services
        ConfigureServices(builder.Services, builder.Configuration);

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        Configure(app, app.Environment);

        app.Run(); // This starts the web server.
    }

    /// <summary>
    /// Configure services.
    /// </summary>
    /// <param name="services">The services<see cref="IServiceCollection"/>.</param>
    /// <param name="configuration">The configuration<see cref="IConfiguration"/>.</param>
    public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        var initializer = new Initializer(configuration, services);
        initializer.Initialize();
        var key = Encoding.ASCII.GetBytes("user_and_admin_crud_operations_test");

        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(x =>
        {
            x.RequireHttpsMetadata = false;
            x.SaveToken = true;
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                RoleClaimType = ClaimTypes.Role
            };
        });

        services.AddAuthorization(options =>
        {
            options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
            options.AddPolicy("UserOnly", policy => policy.RequireRole("User"));
        });

        // Add controllers
        services.AddControllers();

        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        // Enable swagger spec generation
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Employee", Version = "v1" });

            // Define the security scheme for JWT Bearer
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter a valid token",
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                BearerFormat = "JWT",
                Scheme = "Bearer"
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
        });


        // Configure MediatR for CQRS
        services.AddMediatR(typeof(Program).Assembly);

        // Configure CORS
        //services.AddCors(options =>
        //{
        //    options.AddDefaultPolicy(builder =>
        //    {
        //        builder.WithOrigins(configuration.GetSection("AllowedOrigins").Get<string[]>())
        //               .AllowAnyHeader()
        //               .AllowAnyMethod();
        //    });
        //});
    }

    /// <summary>
    /// Configure method for the HTTP request pipeline.
    /// </summary>
    /// <param name="app">The app<see cref="IApplicationBuilder"/>.</param>
    /// <param name="env">The env<see cref="IWebHostEnvironment"/>.</param>
    public static void Configure(WebApplication app, IWebHostEnvironment env)
    {
        app.UseExceptionHandler("/error");

        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Employee"));
        }

        app.UseHttpsRedirection();
        app.UseRouting();

        // Enable CORS
        app.UseCors();

        app.UseAuthorization();
        app.UseWhen(context1 => context1.Request.Path.Value.StartsWith("/api/"), _ =>
        {
            _.Use (async (context, next) =>
            {
            if (context.Request.Query.TryGetValue("access_token", out StringValues token))
            {
                context.Request.Headers.Authorization = $"Bearer {token}";
            }
            await next();
            });
        });

        app.UseMiddleware<JWTMiddleware>();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            
        });
    }
}
