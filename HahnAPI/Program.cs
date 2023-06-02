using Domain.Common;
using Domain.Common.Configurations;
using Infrastructure.Common.Configurations;
using Infrastructure.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHealthChecks();
builder.Services.AddServices();

//AppSettings configs
var config = builder.Configuration;
builder.Services.Configure<JwtBearerConfigs>(builder.Configuration.GetSection(JwtBearerConfigs.Key));
builder.Services.Configure<DatabaseConfigs>(builder.Configuration.GetSection(DatabaseConfigs.Key));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = false;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidAudience = config["JwtSettings:JWT_AUDIENCE_TOKEN"],
        ValidIssuer = config["JwtSettings:JWT_ISSUER_TOKEN"],
        IssuerSigningKey =
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JwtSettings:JWT_SECRET_KEY"]))
    };
});
builder.Services.AddAuthentication();
builder.Services.AddDbContext<HahnAPIContext>(options => options.UseSqlServer(config["ConnectionStrings:HahnConnection"]));

var app = builder.Build();

app.MapHealthChecks("/health");

// Configure the HTTP request pipeline.
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
