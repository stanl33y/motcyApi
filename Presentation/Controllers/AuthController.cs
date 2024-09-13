using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Controller responsible for user authentication.
/// </summary>
[ApiController]
[Route("api/v1/auth")]
public class AuthController : ControllerBase
{
    private readonly IJwtAuthenticationService _authService;

    public AuthController(IJwtAuthenticationService authService)
    {
        _authService = authService;
    }

    /// <summary>
    /// Authenticates a user and returns a JWT token.
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// 
    ///     POST /api/v1/auth/login
    ///     {
    ///        "username": "admin",
    ///        "password": "admin"
    ///     }
    /// 
    /// Test users:
    /// - admin/admin
    /// - deliveryperson/deliveryperson
    /// 
    /// Returns a JWT token if the credentials are valid.
    /// </remarks>
    /// <param name="login">Object containing username and password.</param>
    /// <returns>Returns a JWT token or an authentication error.</returns>
    /// <response code="200">Token successfully generated</response>
    /// <response code="401">Invalid username or password</response>
    [AllowAnonymous]
    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginDTO login)
    {
        if (login.Username == "admin" && login.Password == "admin")
        {
            var token = _authService.GenerateToken(login.Username, "admin");
            return Ok(new { Token = token });
        }

        if (login.Username == "deliveryperson" && login.Password == "deliveryperson")
        {
            var token = _authService.GenerateToken(login.Username, "deliveryperson");
            return Ok(new { Token = token });
        }

        return Unauthorized();
    }
}
