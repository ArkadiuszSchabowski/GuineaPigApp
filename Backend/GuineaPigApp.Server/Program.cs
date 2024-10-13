using GuineaPigApp.Server;
using GuineaPigApp.Server.Database;
using GuineaPigApp.Server.Database.Entities;
using GuineaPigApp.Server.Interfaces;
using GuineaPigApp.Server.Middleware;
using GuineaPigApp.Server.Repositories;
using GuineaPigApp.Server.Seeders;
using GuineaPigApp.Server.Services;
using GuineaPigApp.Server.Validators;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var authenticationSettings = new AuthenticationSettings();

builder.Services.AddSingleton(authenticationSettings);
builder.Configuration.GetSection("Authentication").Bind(authenticationSettings);
builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = "Bearer";
    option.DefaultScheme = "Bearer";
    option.DefaultChallengeScheme = "Bearer";
}).AddJwtBearer(cfg =>
{
    cfg.RequireHttpsMetadata = false;
    cfg.SaveToken = true;
    cfg.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = authenticationSettings.JwtIssuer,
        ValidAudience = authenticationSettings.JwtIssuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey)),
    };
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ErrorHandlingMiddleware>();

builder.Services.AddDbContext<MyDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultDbConnectionString")));

builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IGuineaPigService, GuineaPigService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IGuineaPigRepository, GuineaPigRepository>();
builder.Services.AddScoped<IProductSeederRepository, ProductSeederRepository>();

builder.Services.AddScoped<IAccountSeeder, AccountSeeder>();
builder.Services.AddScoped<IProductSeeder,  ProductSeeder>();

builder.Services.AddScoped<IPaginatorValidator, PaginatorValidator>();
builder.Services.AddScoped<IUserValidator, UserValidator>();

var app = builder.Build();

app.UseCors(x => x.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod());

using (var scope = app.Services.CreateScope())
{
    var accountSeeder = scope.ServiceProvider.GetRequiredService<IAccountSeeder>();
    var productSeeder = scope.ServiceProvider.GetRequiredService<IProductSeeder>();

    accountSeeder.SeedData();
    productSeeder.SeedData();
}

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

public partial class Program { }
