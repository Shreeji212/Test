using ElmahCore.Mvc;
using Employee.BusinessLogic.BusinessLogic;
using Employee.BusinessLogic.IBusinessLogic;
using Employee.Data.Entities;
using Employee.Data.IRepositories;
using Employee.Data.Repositories;
using Employee.WebApi.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();

var connectionString = configuration.GetConnectionString("EmployeeConnection");
builder.Services.AddDbContext<EmployeeManagementContext>(o => o.UseSqlServer(connectionString));

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IUnitOfwork, UnitOfwork>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IEmployeeBusinessLogic, EmployeeBusinessLogic>();

builder.Services.AddElmah<SqlErrorLog>(options =>
{
    options.Path = @"elmah";
    options.ConnectionString = configuration.GetConnectionString("EmployeeConnection");
    options.LogPath = "~/Logs/errors.xml";
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Employee Management",
        Version = "v1",
        Description = "Employee Management",
    });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

// Configure the HTTP request pipeline.

//app.UseHttpsRedirection();

app.UseAuthorization();

app.UseElmah();

app.MapControllers();

app.Run();
