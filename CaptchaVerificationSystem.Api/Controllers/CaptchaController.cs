using CaptchaVerificationSystem.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace CaptchaVerificationSystem.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CaptchaController : ControllerBase
{
    private readonly IServiceManager _serviceManager;

    public CaptchaController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }

    [HttpPost("generate")]
    public async Task<IActionResult> GenerateCaptcha()
    {
        var captcha = await _serviceManager.CaptchaGenerationService.GenerateCaptchaAsync();
        return Ok(captcha);
    }
}