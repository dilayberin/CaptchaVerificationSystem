using CaptchaVerificationSystem.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace CaptchaVerificationSystem.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CaptchaAnalyticsController : ControllerBase
{
    private readonly ICaptchaAnalyticsService _captchaAnalyticsService;

    public CaptchaAnalyticsController(ICaptchaAnalyticsService captchaAnalyticsService)
    {
        _captchaAnalyticsService = captchaAnalyticsService;
    }

    [HttpGet("statistics")]
    public async Task<IActionResult> GetStatistics()
    {
        var statistics = await _captchaAnalyticsService.GetStatisticsAsync();

        return Ok(statistics);
    }
}