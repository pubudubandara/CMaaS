using CMaaS.Backend.Data;
using CMaaS.Backend.Middlewares;
using CMaaS.Backend.Services.Implementations;
using CMaaS.Backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// --- IHttpContextAccessor and UserContextService ---
builder.Services.AddHttpContextAccessor(); // Required to read the token
builder.Services.AddScoped<IUserContextService, UserContextService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// --- Swagger Configuration with JWT Support ---
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "CMaaS API",
        Version = "v1",
        Description = "Content Management as a Service API"
    });

    // Add JWT Authentication to Swagger
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer' [space] and then your valid token.\r\n\r\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...\""
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// PostgreSQL connection
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// --- JWT Authentication Configuration ---
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:SecretKey"] ??
                "your-default-secret-key-minimum-32-characters-long")),
        ValidateIssuer = false,
        ValidIssuer = builder.Configuration["JwtSettings:Issuer"] ?? "cmaas-api",
        ValidateAudience = false,
        ValidAudience = builder.Configuration["JwtSettings:Audience"] ?? "cmaas-client",
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
});

// Register Service Layer (Dependency Injection)
// Authentication Services
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();

// Content Management Services
builder.Services.AddScoped<IContentEntryService, ContentEntryService>();
builder.Services.AddScoped<IContentTypeService, ContentTypeService>();
builder.Services.AddScoped<ITenantService, TenantService>();

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

var app = builder.Build();

// --- Global Exception Middleware (Must be first in pipeline) ---
app.UseMiddleware<ExceptionMiddleware>();
// -------------------------------------------------------------

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// --- Important: Authentication must come before Authorization ---
app.UseAuthentication();
app.UseAuthorization();

app.UseCors("AllowAll");

app.MapControllers();

app.Run();