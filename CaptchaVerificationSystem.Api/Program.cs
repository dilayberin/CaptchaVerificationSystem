using CaptchaVerificationSystem.Application.Interfaces.Services;
using CaptchaVerificationSystem.Application.Services;
using CaptchaVerificationSystem.Application.Interfaces.Repositories;
using CaptchaVerificationSystem.Persistence.Repositories;
using CaptchaVerificationSystem.Persistence.Context;

using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// --------------------
// CONTROLLERS
// --------------------
builder.Services.AddControllers();

// --------------------
// SWAGGER
// --------------------
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// --------------------
// DATABASE (POSTGRESQL)
// --------------------
builder.Services.AddDbContext<CaptchaDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// --------------------
// REPOSITORIES
// --------------------
builder.Services.AddScoped<IRepositoryManager, RepositoryManager>();

// --------------------
// SERVICES
// --------------------
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IImageService, ImageService>();
builder.Services.AddScoped<ICaptchaAttemptService, CaptchaAttemptService>();
builder.Services.AddScoped<ICaptchaGenerationService, CaptchaGenerationService>();
builder.Services.AddScoped<IServiceManager, ServiceManager>();
builder.Services.AddScoped<IRepositoryManager, RepositoryManager>();

var app = builder.Build();

// --------------------
// MIDDLEWARE PIPELINE
// --------------------

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();