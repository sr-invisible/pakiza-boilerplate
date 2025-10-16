
namespace Pakiza.API.Controllers.Common;

[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public abstract class BaseApiController(IMediator mediator) : ControllerBase
{
    protected readonly IMediator _mediator = mediator;

    protected void SetRefreshTokenCookie(string refreshToken, int days = 7)
    {
        Response.Cookies.Append("refreshToken", refreshToken, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = DateTimeOffset.UtcNow.AddDays(days)
        });
    }

    protected string? GetRefreshTokenFromCookie()
    {
        return Request.Cookies.TryGetValue("refreshToken", out var token) && !string.IsNullOrWhiteSpace(token)
            ? token
            : null;
    }

    protected void DeleteRefreshTokenCookie() => Response.Cookies.Delete("refreshToken");

    protected IActionResult ProblemResponse(string message, int statusCode = StatusCodes.Status400BadRequest)
    {
        return Problem(title: message, statusCode: statusCode);
    }
}

