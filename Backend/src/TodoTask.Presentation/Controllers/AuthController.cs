using Microsoft.AspNetCore.Mvc;
using TodoTask.Application.DTOs;
using TodoTask.Application.Handlers.Auth.Commands.Login;
using TodoTask.Contracts.Requests;
using Wolverine;

namespace TodoTask.Presentation.Controllers;

/// <summary>
/// Контроллер аутентификации.
/// </summary>
[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    private readonly IMessageBus _messageBus;

    public AuthController(IMessageBus messageBus)
    {
        _messageBus = messageBus;
    }
    
    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var token = await _messageBus.InvokeAsync<LoginResponseDto>(new LoginCommand(request.UserName));

        return Ok(new { token });
    }
}