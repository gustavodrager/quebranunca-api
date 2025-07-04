using Microsoft.AspNetCore.Mvc;
using QNF.Plataforma.Application.Commands.Auth;
using QNF.Plataforma.Application.DTOs;
using QNF.Plataforma.Application.Interfaces;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly ICommandHandler<LoginCommand, LoginResponse> _loginHandler;
    private readonly ICommandHandler<RefreshTokenCommand, RefreshTokenResponse> _refreshHandler;

    public AuthController(
        ICommandHandler<LoginCommand, LoginResponse> loginHandler,
        ICommandHandler<RefreshTokenCommand, RefreshTokenResponse> refreshHandler)
    {
        _loginHandler = loginHandler;
        _refreshHandler = refreshHandler;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var command = new LoginCommand(request.Email, request.Password);
        var response = await _loginHandler.HandleAsync(command);
        
        return Ok(response);
    }

    [HttpPut("token")]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var command = new RefreshTokenCommand(request.RefreshToken);
        var result = await _refreshHandler.HandleAsync(command);

        return Ok(result);
    }

}