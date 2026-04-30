using CaptchaVerificationSystem.Application.DTOs.CategoryDtos;
using CaptchaVerificationSystem.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace CaptchaVerificationSystem.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryController : ControllerBase //temel controller sınıfı,ASP.NET Core içinden gelir
{
    private readonly IServiceManager _serviceManager;

    public CategoryController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }

    [HttpGet]
    public async Task<IActionResult> GetActiveCategories()
    {
        var categories = await _serviceManager.CategoryService.GetActiveCategoriesAsync();
        return Ok(categories);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryDto dto) //request’in body kısmından veri alır
    {
        var result = await _serviceManager.CategoryService.CreateAsync(dto);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var category = await _serviceManager.CategoryService.GetByIdAsync(id);

        if (category == null)
            return NotFound();

        return Ok(category);
    }

    [HttpPut("{id}")] //GÜNCELLEME
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateCategoryDto dto)
    {
        await _serviceManager.CategoryService.UpdateAsync(id, dto);
        return NoContent();
    }
}