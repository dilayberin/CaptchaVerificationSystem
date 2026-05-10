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

    public CaptchaController(
        IServiceManager serviceManager,
        CaptchaFileService fileService)
    {
        _serviceManager = serviceManager;
        _fileService = fileService;
    }

    [HttpPost("generate")]
    public async Task<IActionResult> GenerateCaptcha()
    {
        var captcha =
            await _serviceManager
                .CaptchaGenerationService
                .GenerateCaptchaAsync();

        return Ok(captcha);
    }

    [HttpPost("verify")]
    public async Task<IActionResult> VerifyCaptcha(
        [FromBody] VerifyCaptchaRequestDto request)
    {
        Console.WriteLine(request.ChallengeId);

        var forwardedIp =
            Request.Headers["X-Forwarded-For"].ToString();

        var ipAddress =
            !string.IsNullOrEmpty(forwardedIp)
                ? forwardedIp
                : HttpContext.Connection.RemoteIpAddress?.ToString();
        
        var userAgent =
            HttpContext.Request.Headers.UserAgent.ToString();

        Console.WriteLine(userAgent);

        var result =
            await _serviceManager
                .CaptchaAttemptService
                .VerifyCaptchaAsync(
                    request.ChallengeId,
                    request.SelectedChallengeImageIds,
                    request.ResponseTimeMs,
                    ipAddress,
                    userAgent
                );

        return Ok(result);
    }

    [HttpPost("generate-file")]
    public async Task<IActionResult> GenerateCaptchaFromFiles()
    {
        var result =
            await _fileService.GenerateCaptchaAsync();

        return Ok(result);
    }
}