using Azure.Identity;
using Data.Contexts;
using Data.Interfaces;
using Data.Repositories;
using Infrastructure.Services;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

builder.Configuration.AddAzureKeyVault(new Uri("https://group-project-keyvault.vault.azure.net/"), new DefaultAzureCredential());

var dbConnectionString = builder.Configuration["DbConnectionString-GroupProject"];
var jwtPublicKey = builder.Configuration["JwtPublicKey"];
var jwtIssuer = builder.Configuration["JwtIssuer"];
var jwtAudience = builder.Configuration["JwtAudience"];

builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer(dbConnectionString));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
  options.RequireHttpsMetadata = true;
  options.TokenValidationParameters = new TokenValidationParameters
  {
    ValidateIssuer = true,
    ValidIssuer = builder.Configuration["JwtIssuer"],
    ValidateAudience = true,
    ValidAudience = builder.Configuration["JwtAudience"],
    ValidateIssuerSigningKey = true,
    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtPublicKey"]!)),
    RoleClaimType = ClaimTypes.Role
  };
  options.MapInboundClaims = true;
});

builder.Services.AddScoped<IGymClassRepository, GymClassRepository>();
builder.Services.AddScoped<IGymClassService, GymClassService>();

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
  var db = scope.ServiceProvider.GetRequiredService<DataContext>();
  db.Database.Migrate();
}
app.MapOpenApi();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
  c.SwaggerEndpoint("/swagger/v1/swagger.json", "Gym Class API");
  c.RoutePrefix = string.Empty;
});
app.UseHttpsRedirection();
app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();