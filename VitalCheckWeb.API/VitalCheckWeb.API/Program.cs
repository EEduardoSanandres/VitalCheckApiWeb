using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using VitalCheckWeb.API.Security.Authorization.Handlers.Implementations;
using VitalCheckWeb.API.Security.Authorization.Handlers.Interfaces;
using VitalCheckWeb.API.Security.Authorization.Middleware;
using VitalCheckWeb.API.Security.Authorization.Settings;
using VitalCheckWeb.API.Security.Domain.Services;
using VitalCheckWeb.API.Security.Services;
using VitalCheckWeb.API.Shared.Persistence.Contexts;
using VitalCheckWeb.API.Shared.Persistence.Repositories;
using VitalCheckWeb.API.VitalCheck.Domain.Repositories;
using VitalCheckWeb.API.VitalCheck.Domain.Services;
using VitalCheckWeb.API.VitalCheck.Persistence.Repositories;
using VitalCheckWeb.API.VitalCheck.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
 // Add API Documentation Information
 options.SwaggerDoc("v1", new OpenApiInfo
 {
  Version = "v1",
  Title = "Vital Check API",
  Description = "Vital Check API RESTful API",
  TermsOfService = new Uri("https://vital-check.com/tos"),
  Contact = new OpenApiContact
  {
   Name = "VITAL.check",
   Url = new Uri("https://vital.check")
  },
  License = new OpenApiLicense
  {
   Name = "Vital Check Resources License",
   Url = new Uri("https://vital-check.com/license")
  }
 });
 options.EnableAnnotations();
 options.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme
 {
  Type = SecuritySchemeType.Http,
  Scheme = "bearer",
  BearerFormat = "JWT",
  Description = "JWT Authorization header using the Bearer scheme."
 });
 options.AddSecurityRequirement(new OpenApiSecurityRequirement
 {
  {
   new OpenApiSecurityScheme
   {
    Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "bearerAuth" }
   },
   Array.Empty<string>()
  }
 });
});

// Add Database Connection

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(
 options => options.UseMySQL(connectionString)
 .LogTo(Console.WriteLine, LogLevel.Information)
 .EnableSensitiveDataLogging()
 .EnableDetailedErrors());

// Add lowercase routes

builder.Services.AddRouting(options => options.LowercaseUrls = true);

// Dependency Injection Configuration}

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IJwtHandler, JwtHandler>();

// AppSettings Configuration
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<IClientService, ClientService>();

builder.Services.AddScoped<IDispatchRepository, DispatchRepository>();
builder.Services.AddScoped<IDispatchService, DispatchService>();

builder.Services.AddScoped<IInventoryRepository, InventoryRepository>();
builder.Services.AddScoped<IInventoryService, InventoryService>();

builder.Services.AddScoped<IMedicineRepository, MedicineRepository>();
builder.Services.AddScoped<IMedicineService, MedicineService>();

builder.Services.AddScoped<IMedicineTypeRepository, MedicineTypeRepository>();
builder.Services.AddScoped<IMedicineTypeService, MedicineTypeService>();

builder.Services.AddScoped<ISaleRepository, SaleRepository>();
builder.Services.AddScoped<ISaleService, SaleService>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IUserPlanRepository, UserPlanRepository>();
builder.Services.AddScoped<IUserPlanService, UserPlanService>();

builder.Services.AddScoped<IUserTypeRepository, UserTypeRepository>();
builder.Services.AddScoped<IUserTypeService, UserTypeService>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

// AutoMapper Configuration

builder.Services.AddAutoMapper(
 typeof(VitalCheckWeb.API.VitalCheck.Mapping.ModelToResourceProfile),
 typeof(VitalCheckWeb.API.Security.Mapping.ModelToResourceProfile),
 typeof(VitalCheckWeb.API.VitalCheck.Mapping.ResourceToModelProfile),
 typeof(VitalCheckWeb.API.Security.Mapping.ResourceToModelProfile));


builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

var app = builder.Build();

// Validation for ensuring Database Objects are created

using (var scope = app.Services.CreateScope())
using (var context = scope.ServiceProvider.GetService<AppDbContext>())
{
 context.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{ 
 app.UseSwagger();
 app.UseSwaggerUI(options =>
 {
  options.SwaggerEndpoint("v1/swagger.json", "v1");
  options.RoutePrefix = "swagger";
 });
}

app.UseCors(x => x
 .AllowAnyOrigin()
 .AllowAnyMethod()
 .AllowAnyHeader());

// Configure Error Handler Middleware
app.UseMiddleware<ErrorHandlerMiddleware>();
// Configure JWT Handling
app.UseMiddleware<JwtMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
