using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;

using bankingApplication_API.Data;
using bankingApplication_API.Interfaces;
using bankingApplication_API.Repository;
using System.Reflection;

// WebApplication - a class representing the main application object.
// It aggregates various components and functions that are frequently used in a web application configuration.
WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

ILoggerFactory loggerFactory = LoggerFactory.Create(builder => builder.AddFile("Logs/log_{Date}.txt"));
ILogger<Program> _logger = loggerFactory.CreateLogger<Program>();

// Add services to the container.
builder.Services.AddCors(options =>
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

builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<INaturalPersonInterface, NaturalPersonRepository>();
builder.Services.AddScoped<IJuridicalPersonInterface, JuridicalPersonRepository>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string connectionString = "Data Source=(local);Initial Catalog=Bank;Integrated Security=True;Connect Timeout=30;Encrypt=False;" +
    "Trust Server Certificate=False;Application Intent=ReadWrite;MultiSubnetFailover=False\r\n";

try
{
    builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(connectionString));
}
catch (Exception ex)
{
    Console.WriteLine("An error occured while connecting to the database: " + ex.Message);
    _logger.LogError(ex.Message);
    loggerFactory.Dispose();
    Environment.Exit(1);
}

builder.Services.Configure<FormOptions>(options => options.MultipartBodyLengthLimit = long.MaxValue);
builder.Services.AddMvc().AddMvcOptions(options =>
{
    options.EnableEndpointRouting = false;
    options.Filters.Add(new ConsumesAttribute("multipart/form-data"));
});

WebApplication app = builder.Build(); //builds instance of WebApplication from WebApplicationBuilder's object

// Configure the HTTP request pipeline.
// Defines how the app handles incoming HTTP requests. 
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAny");
app.UseRouting();
app.UseAuthorization();
app.UseMvc();

using (IServiceScope scope = app.Services.CreateScope())
{
    IServiceProvider serviceProvider = scope.ServiceProvider;
    DataContext dbContext = serviceProvider.GetRequiredService<DataContext>();

    try
    {
        // Use reflection to get DbSet properties from the DbContext
        IEnumerable<PropertyInfo> dbSetProperties = dbContext.GetType().GetProperties()
            .Where(
                p => p.PropertyType.IsGenericType 
                    && p.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>)
            );

        foreach (PropertyInfo property in dbSetProperties)
        {
            Type entityType = property.PropertyType.GetGenericArguments().First();
            string tableName = dbContext.Model.FindEntityType(entityType).GetTableName();
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine("An error occured while processing database query: " + ex.Message);
        _logger.LogError(ex.Message);
        loggerFactory.Dispose();
        Environment.Exit(1);
    }
}

app.Run();
