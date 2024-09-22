using GuineaPigApp.Server.Database;
using GuineaPigApp.Server.Interfaces;
using GuineaPigApp.Server.Middleware;
using GuineaPigApp.Server.Repositories;
using GuineaPigApp.Server.Services;
using GuineaPigApp.Server.Validators;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ErrorHandlingMiddleware>();

builder.Services.AddDbContext<MyDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultDbConnectionString")));

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductValidator, ProductValidator>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IGuineaPigService, GuineaPigService>();

var app = builder.Build();

if (app.Environment.IsProduction())
{
app.UseMiddleware<ErrorHandlingMiddleware>();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
