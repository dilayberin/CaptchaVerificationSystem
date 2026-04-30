using CaptchaVerificationSystem.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using CaptchaVerificationSystem.Application.DTOs.CaptchaDtos;

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
    [HttpPost("verify")]
    public async Task<IActionResult> VerifyCaptcha([FromBody] VerifyCaptchaRequestDto request)
    {
        var result = await _serviceManager.CaptchaAttemptService.VerifyCaptchaAsync(
            request.ChallengeId,
            request.SelectedChallengeImageIds,
            request.ResponseTimeMs
        );

        return Ok(new
        {
            success = result
        });
    }
}