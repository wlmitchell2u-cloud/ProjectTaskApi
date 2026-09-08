using EnterpriseTaskManager.Api.Hubs;
using EnterpriseTaskManager.Api.Middleware;
using EnterpriseTaskManager.Application.Auth;
using EnterpriseTaskManager.Application.Mapping;
using EnterpriseTaskManager.Application.Services.Interfaces;
using EnterpriseTaskManager.Infrastructure.Persistence;
using EnterpriseTaskManager.Infrastructure.Services.Auth;
using EnterpriseTaskManager.Infrastructure.Services.Implementations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using EnterpriseTaskManager.Infrastructure.Seeding;
//using Microsoft.OpenApi
using System.Text;


var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<JwtSettings>(
    builder.Configuration.GetSection("JwtSettings"));

//AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

// DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Application Services
builder.Services.AddScoped<IProjectService, EnterpriseTaskManager.Infrastructure.Services.Implementations.ProjectService>();
builder.Services.AddScoped<ITaskService, EnterpriseTaskManager.Infrastructure.Services.Implementations.TaskService>();
builder.Services.AddScoped<IUserService, EnterpriseTaskManager.Infrastructure.Services.Implementations.UserService>();
builder.Services.AddScoped<ITagService, EnterpriseTaskManager.Infrastructure.Services.Implementations.TagService>();
builder.Services.AddScoped<ITaskCommentService, EnterpriseTaskManager.Infrastructure.Services.Implementations.TaskCommentService>();
builder.Services.AddScoped<IAuthService, AuthService>();


// Add services to the container.

//.Services.AddControllers();
builder.Services.AddControllers(options =>
{
    options.Filters.Add(new Microsoft.AspNetCore.Mvc.Authorization.AuthorizeFilter());
});




builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    var jwtSettings = builder.Configuration.GetSection("JwtSettings");

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(jwtSettings["Key"]!))
    };
});


// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins(
            "http://localhost:5173",
            "http://localhost:5174",
            "http://localhost:5175"
        )
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

builder.Services.AddSignalR();



var app = builder.Build();

// Seed sample data (users, etc.)
await DatabaseSeeder.SeedAsync(app.Services);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    //app.MapSwagger();
    //app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseCors("AllowFrontend");

app.UseMiddleware<GlobalExceptionMiddleware>();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.MapHub<ProjectHub>("/hubs/projects");

app.Run();
