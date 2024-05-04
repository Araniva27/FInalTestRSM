using Microsoft.EntityFrameworkCore;
using FinalTestRSM.Infraestructure;
using FinalTestRSM.Interfaces;
using FinalTestRSM.Services;
using FinalTestRSM.Infraestructure.Repositories;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Final Test API",
        Description = "API for data retrieval and report generation",                
    });

    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

builder.Services.AddDbContext<AdvWorksDbContext>(options =>
{
    // Configure the database context to use SQL Server
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
        opt => opt.MigrationsAssembly(typeof(AdvWorksDbContext).Assembly.FullName));// Configure additional options for SQL Server
});

builder.Services.AddTransient<ISalesReportRepository, SaleReportRepository>();
builder.Services.AddTransient<ISalesReportService, SalesReportServices>();
builder.Services.AddTransient<IProductCategoryRepository, ProductCategoryRepository>();
builder.Services.AddTransient<IProductCategoryService, ProductCategoryService>();
builder.Services.AddTransient<ISalesByRegionReportRepository, SaleByRegionReportRepository>();
builder.Services.AddTransient<ISalesByRegionReportService, SalesByRegionReportService>();
builder.Services.AddTransient<ISalesTerritoryRepository, SaleTerritoryRepository>();
builder.Services.AddTransient<ISalesTerritoryService, SaleTerritoryService>();
builder.Services.AddTransient<ISalesByCustomerRepository, SalesByCustomerRepository>();
builder.Services.AddTransient<ISalesByCustomerService, SalesByCustomerService>();

// CORS (Cross-Origin Resource Sharing) configuration
builder.Services.AddCors(options =>
{
    // Add a default CORS policy
    options.AddDefaultPolicy(
        builder =>
        {
            // Allow any origin (domain), method, and header
            builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseSwaggerUI(options => // UseSwaggerUI is called only in Development.
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors();

app.Run();
