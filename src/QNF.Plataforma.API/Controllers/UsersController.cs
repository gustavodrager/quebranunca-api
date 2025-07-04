using System;
using Microsoft.AspNetCore.Mvc;
using QNF.Plataforma.Application.Commands.User;
using QNF.Plataforma.Application.DTOs;
using QNF.Plataforma.Application.Interfaces;

namespace QNF.Plataforma.API.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly ICommandHandler<RegisterUserCommand, RegisterUserResponse> _registerHandler;

    public UsersController(ICommandHandler<RegisterUserCommand, RegisterUserResponse> registerHandler)
    {
        _registerHandler = registerHandler;
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterUserRequest request)
    {
        var command = new RegisterUserCommand(request);
        var response = await _registerHandler.HandleAsync(command);

        return CreatedAtAction(nameof(Register), new { id = response.UserId }, response);
    }
}
