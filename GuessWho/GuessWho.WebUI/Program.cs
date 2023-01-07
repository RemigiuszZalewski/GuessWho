using System.Reflection;
using System.Text;
using FluentValidation;
using FluentValidation.AspNetCore;
using GuessWho.Application.Core.Abstractions;
using GuessWho.Application.Core.Validations;
using GuessWho.Application.Generators;
using GuessWho.Application.Services;
using GuessWho.Application.Validators;
using GuessWho.Domain.Generators;
using GuessWho.Domain.Repositories;
using GuessWho.Domain.Requests;
using GuessWho.Domain.Services;
using GuessWho.Domain.Validators;
using GuessWho.Infrastructure;
using GuessWho.Infrastructure.Authentication;
using GuessWho.Persistence;
using GuessWho.Persistence.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;



var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddScoped<IValidator<LoginRequest>, LoginRequestValidator>();
builder.Services.AddScoped<IValidator<RegisterRequest>, RegisterRequestValidator>();
    
builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer
    (builder.Configuration.GetConnectionString("DbConnectionString")));

builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection(JwtOptions.SettingsKey));

builder.Services.AddScoped<ISessionService, SessionService>();
builder.Services.AddScoped<IPlayerService, PlayerService>();
builder.Services.AddScoped<ISessionCodeGenerator, SessionCodeGenerator>();
builder.Services.AddScoped<ISessionValidator, SessionValidator>();
builder.Services.AddScoped<ISessionRepository, SessionRepository>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JwtOptions:Issuer"],
        ValidAudience = builder.Configuration["JwtOptions:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["JwtOptions:SecurityKey"] ?? string.Empty))
    });

builder.Services.AddScoped<IJwtGenerator, JwtGenerator>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("GuessWhoCorsPolicy", policy =>
    {
        policy.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod();
    });
});

var app = builder.Build();

using var context = app.Services.GetService<ApplicationDbContext>();

await context?.Database.MigrateAsync();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseCors("GuessWhoCorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();