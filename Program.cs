using Microsoft.EntityFrameworkCore;
using bankingApplication_API.Data;
using bankingApplication_API.Interfaces;
using bankingApplication_API.Repository;

// WebApplication - a class representing the main application object.
// It aggregates various components and functions that are frequently used in a web application configuration.
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowOrigin",
        builder => builder.WithOrigins("http://localhost:8100")
                          .AllowAnyHeader()
                          .AllowAnyMethod());
});
builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<INaturalPersonInterface, NaturalPersonRepository>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = "Data Source=GAMING;Initial Catalog=Bank;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;MultiSubnetFailover=False\r\n";
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(connectionString);
});

var app = builder.Build(); //builds instance of WebApplication from WebApplicationBuilder's object

// Configure the HTTP request pipeline.
// Defines how the app handles incoming HTTP requests. 
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseCors("AllowOrigin");
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    var dbContext = serviceProvider.GetRequiredService<DataContext>();

    // Use reflection to get DbSet properties from the DbContext
    var dbSetProperties = dbContext.GetType().GetProperties()
        .Where(p => p.PropertyType.IsGenericType && p.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>));

    foreach (var property in dbSetProperties)
    {
        var entityType = property.PropertyType.GetGenericArguments().First();
        var tableName = dbContext.Model.FindEntityType(entityType).GetTableName();
    }
}

app.Run();
