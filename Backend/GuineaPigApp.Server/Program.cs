using GuineaPigApp.Server;
using GuineaPigApp.Server.Database;
using GuineaPigApp.Server.Database.Entities;
using GuineaPigApp.Server.Interfaces;
using GuineaPigApp.Server.Middleware;
using GuineaPigApp.Server.Repositories;
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

builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IProductValidator, ProductValidator>();

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
