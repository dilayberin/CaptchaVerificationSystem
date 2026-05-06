using System.Text.Json.Serialization;
using CaptchaVerificationSystem.Api.FileServices;
using CaptchaVerificationSystem.Application.Interfaces.Services;
using CaptchaVerificationSystem.Application.Services;
using CaptchaVerificationSystem.Application.Interfaces.Repositories;
using CaptchaVerificationSystem.Persistence.Repositories;
using CaptchaVerificationSystem.Persistence.Context;

using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services
    .AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<CaptchaDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IRepositoryManager, RepositoryManager>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IImageService, ImageService>();
builder.Services.AddScoped<ICaptchaAttemptService, CaptchaAttemptService>();
builder.Services.AddScoped<ICaptchaGenerationService, CaptchaGenerationService>();
builder.Services.AddScoped<IServiceManager, ServiceManager>();
builder.Services.AddScoped<IRepositoryManager, RepositoryManager>();
builder.Services.AddScoped<ICaptchaAttemptService, CaptchaAttemptService>();
builder.Services.AddScoped<ICaptchaAnalyticsService, CaptchaAnalyticsService>();
builder.Services.AddScoped<CaptchaFileService>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/", () => Results.Redirect("/swagger")).ExcludeFromDescription();

app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();