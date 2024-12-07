using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;

using bankingApplication_API.Data;
using bankingApplication_API.Interfaces;
using bankingApplication_API.Repository;

public static class StartupExtensions
{
    public static void AddStartupServices(this IServiceCollection services, IConfiguration configuration)
    {
        // CORS
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAny",
                builder => builder.AllowAnyOrigin()
                                  .AllowAnyHeader()
                                  .AllowAnyMethod());

            options.AddPolicy("AllowSpecificOrigin",
                builder =>
                {
                    builder.WithOrigins("http://localhost:8100")
                           .AllowAnyHeader()
                           .AllowAnyMethod();
                });
        });

        // Add controllers
        services.AddControllers();
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        // Scoped services
        services.AddScoped<INaturalPersonInterface, NaturalPersonRepository>();
        services.AddScoped<IJuridicalPersonInterface, JuridicalPersonRepository>();

        // Swagger
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        // Database context
        string connectionString = "Data Source=(local);" +
            "Initial Catalog=Bank;" +
            "Integrated Security=True;" +
            "Connect Timeout=30;" +
            "Encrypt=False;" +
            "Trust Server Certificate=False;" +
            "Application Intent=ReadWrite;" +
            "MultiSubnetFailover=False\r\n";

        services.AddDbContext<DataContext>(
            options => options.UseSqlServer(connectionString)
        );

        // Configure FormOptions for file uploads
        services.Configure<FormOptions>(
            options => options.MultipartBodyLengthLimit = long.MaxValue
        );

        // MVC
        services.AddMvc().AddMvcOptions(
            options =>
            {
                options.EnableEndpointRouting = false;
                options.Filters.Add(
                    new ConsumesAttribute("multipart/form-data")
                );
            }
        );
    }
}
