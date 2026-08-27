using CareHomeLeaveManagement.Application.Departments.Interfaces;
using CareHomeLeaveManagement.Application.Departments.Services;
using CareHomeLeaveManagement.Infrastructure.Persistence;
using CareHomeLeaveManagement.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using CareHomeLeaveManagement.Application;
using CareHomeLeaveManagement.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplication();

builder.Services.AddInfrastructure(builder.Configuration);


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<CareHomeLeaveManagementDbContext>(
    options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CareHomeDatabase")));

    var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
