using Pakiza.Application.DTOs.User;
using Pakiza.Application.Features.Authentication.Commands;

namespace Pakiza.Web.Controllers
{
    public class AuthenticationController : BaseMvcController
    {
        public AuthenticationController(IMediator mediator) : base(mediator) { }

        [AllowAnonymous]
        public IActionResult Register() => View();

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterDTO model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await _mediator.Send(new RegisterCommand(model));

            return RedirectToAction("Login");
        }

        [AllowAnonymous]
        public IActionResult Login() => View();

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginDTO model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await _mediator.Send(new LoginCommand(model));

            if (result == null)
                return ProblemResponse("Invalid credentials", StatusCodes.Status401Unauthorized);

            // ✅ Build Claims
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, result.UserResponse.User.Id.ToString()),
                new Claim(ClaimTypes.Email, result.UserResponse.User.Email ?? "")
            };

            // ✅ Add role claims if available
            if (result.UserResponse.User.Roles != null)
            {
                foreach (var role in result.UserResponse.User.Roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }
            }

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            // ✅ Sign in user using cookie authentication
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            // ✅ Set refresh token cookie
            SetRefreshTokenCookie(result.UserResponse.Token.RefreshToken);

            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            var refreshToken = GetRefreshTokenFromCookie();
            if (refreshToken is null)
                return ProblemResponse("Refresh token is missing");

            await _mediator.Send(new LogoutCommand(refreshToken));

            // ✅ Delete the refresh token and sign out
            DeleteRefreshTokenCookie();
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login");
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Refresh(string accessToken)
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
            return Json(result);
        }
    }
}
