using Business.Abstract;
using Business.Concrete;
using Business.Helpers.JWT;
using Business.Helpers.Mapper;
using Business.Validation;
using DataAccess;
using DataAccess.Abstract;
using DataAccess.Concrete;
using Entity.DTOs;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddValidatorsFromAssemblyContaining<UserValidator<UserForRegisterDto>>();
builder.Services.AddDbContext<LoginSampleContext>();
builder.Services.AddAutoMapper(typeof(UserProfile));

builder.Services.AddScoped<IUserDal, EfUserDal>();
builder.Services.AddScoped<IUserRolesDal, EfUserRoles>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITokenHelper, JWTHelper>();
builder.Services.AddScoped<UserForRegisterValidator>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,   
        ValidateIssuerSigningKey = true
    };
});

builder.Services.AddAuthorization();

builder.Services.AddControllers();

builder.Services.AddCors();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(builder => builder.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()
.WithMethods("GET", "POST").DisallowCredentials());

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();   

app.Run();