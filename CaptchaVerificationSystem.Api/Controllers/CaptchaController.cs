using CaptchaVerificationSystem.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using CaptchaVerificationSystem.Application.DTOs.CaptchaDtos;
using CaptchaVerificationSystem.Api.FileServices;

namespace CaptchaVerificationSystem.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CaptchaController : ControllerBase
{
    private readonly IServiceManager _serviceManager;
    private readonly CaptchaFileService _fileService;

    public CaptchaController(IServiceManager serviceManager, CaptchaFileService fileService)
    {
        _serviceManager = serviceManager;
        _fileService = fileService;
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
    [HttpPost("generate-file")]
    public async Task<IActionResult> GenerateCaptchaFromFiles()
    {
        var result = await _fileService.GenerateCaptchaAsync();
        return Ok(result);
    }
}