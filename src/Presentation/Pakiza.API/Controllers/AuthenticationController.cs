using Pakiza.Application.DTOs.User;
using Pakiza.Application.Features.Authentication.Commands;

namespace Pakiza.API.Controllers;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class AuthenticationController(IMediator mediator) : BaseApiController(mediator)
{
    [AllowAnonymous]
    [HttpPost("register")]
    [ProducesResponseType(typeof(AuthenticationResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register([FromBody] RegisterDTO model)
    {
        if (!ModelState.IsValid)
            return ProblemResponse("Invalid registration data");

        var result = await _mediator.Send(new RegisterCommand(model));
        return Ok(result);
    }

    [AllowAnonymous]
    [HttpPost("login")]
    [ProducesResponseType(typeof(AuthenticationResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login([FromBody] LoginDTO model)
    {
        if (!ModelState.IsValid)
            return ProblemResponse("Invalid login credentials", StatusCodes.Status401Unauthorized);

        var result = await _mediator.Send(new LoginCommand(model));

        SetRefreshTokenCookie(result.UserResponse.Token.RefreshToken);
        return Ok(result);
    }

    [Authorize]
    [HttpPost("logout")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Logout()
    {
        var refreshToken = GetRefreshTokenFromCookie();
        if (refreshToken is null)
            return ProblemResponse("Refresh token is missing", StatusCodes.Status400BadRequest);

        var result = await _mediator.Send(new LogoutCommand(refreshToken));
        DeleteRefreshTokenCookie();

        return Ok(result);
    }

    [AllowAnonymous]
    [HttpPut("refresh")]
    [ProducesResponseType(typeof(TokenResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Refresh([FromBody] string accessToken)
    {
        var refreshToken = GetRefreshTokenFromCookie();
        if (refreshToken is null)
            return ProblemResponse("Refresh token is missing", StatusCodes.Status401Unauthorized);

        var result = await _mediator.Send(new RefreshCommand(new TokenRequest
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken
        }));

        SetRefreshTokenCookie(result.TokenResponse.RefreshToken);
        return Ok(result);
    }
}



