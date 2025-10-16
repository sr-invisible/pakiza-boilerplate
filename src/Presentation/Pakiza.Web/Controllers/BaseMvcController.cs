
namespace Pakiza.Web.Controllers;

[Authorize(AuthenticationSchemes = "Cookies")]
public abstract class BaseMvcController : Controller
{
    protected readonly IMediator _mediator;

    protected BaseMvcController(IMediator mediator)
    {
        _mediator = mediator;
    }

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

    protected void DeleteRefreshTokenCookie()
    {
        Response.Cookies.Delete("refreshToken");
    }

    protected IActionResult ProblemResponse(string message, int statusCode = StatusCodes.Status400BadRequest)
    {
        if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
        {
            return StatusCode(statusCode, new { error = message });
        }

        ViewBag.ErrorMessage = message;
        Response.StatusCode = statusCode;
        return View("Error");
    }
}
