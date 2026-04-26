using CaptchaVerificationSystem.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace CaptchaVerificationSystem.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ImageController : ControllerBase
{
    private readonly IServiceManager _serviceManager;

    public ImageController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllActiveImages()
    {
        var images = await _serviceManager.ImageService.GetAllActiveImagesAsync();
        return Ok(images);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var image = await _serviceManager.ImageService.GetByIdAsync(id);

        if (image == null)
            return NotFound();

        return Ok(image);
    }

    [HttpGet("category/{categoryId}")]
    public async Task<IActionResult> GetImagesByCategory(Guid categoryId)
    {
        var images = await _serviceManager.ImageService.GetImagesByCategoryIdAsync(categoryId);
        return Ok(images);
    }
}