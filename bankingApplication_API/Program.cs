using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using bankingApplication_API.Data;
using bankingApplication_API.Interfaces;
using bankingApplication_API.Repository;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

ILoggerFactory loggerFactory = LoggerFactory.Create(builder => builder.AddFile("Logs/log_{Date}.txt"));
ILogger<Program> _logger = loggerFactory.CreateLogger<Program>();

// Add services to the container.
builder.Services.AddStartupServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
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
        Console.WriteLine("An error occurred while processing database query: " + ex.Message);
        _logger.LogError(ex.Message);
        loggerFactory.Dispose();
        Environment.Exit(1);
    }
}

app.Run();
