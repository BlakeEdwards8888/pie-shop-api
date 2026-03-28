using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PieShop.API.DbContexts;
using PieShop.API.Profiles;
using PieShop.API.Services;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

if (environment == Environments.Development)
{
    builder.Host.UseSerilog((context, loggerConfiguration) => loggerConfiguration
    .MinimumLevel.Debug()
    .WriteTo.Console());
}
else
{
    builder.Host.UseSerilog((context, loggerConfiguration) => loggerConfiguration
        .MinimumLevel.Debug()
        .WriteTo.Console()
        .WriteTo.File("logs/cityinfo.txt", rollingInterval: RollingInterval.Day)
        .WriteTo.ApplicationInsights(new TelemetryConfiguration()
        {
            ConnectionString = builder.Configuration["ApplicationInsightsConnectionString"]
        }, TelemetryConverter.Traces));
}

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<PieShopContext>(dbContextOptions
    => {
        dbContextOptions.UseSqlite(builder.Configuration["ConnectionStrings:PieShopDBConnectionString"]);
    });

builder.Services.AddScoped<IPieShopRepository, PieShopRepository>();

builder.Services.AddAutoMapper(configAction => configAction.AddProfile<PieProfile>());

builder.Services.AddAuthentication("Bearer").AddJwtBearer(options =>
{
    options.TokenValidationParameters = new()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Authentication:Issuer"],
        ValidAudience = builder.Configuration["Authentication:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Convert.FromBase64String(builder.Configuration["Authentication:SecretForKey"]))
    };
});

builder.Services.AddAuthorizationBuilder().AddPolicy("Admin", policy => policy.RequireClaim("customrole", "admin"));

var app = builder.Build();

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
