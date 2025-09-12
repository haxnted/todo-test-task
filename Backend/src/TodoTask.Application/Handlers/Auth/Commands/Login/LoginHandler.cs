using TodoTask.Application.AppServices.Abstractions;
using TodoTask.Application.DTOs;

namespace TodoTask.Application.Handlers.Auth.Commands.Login;

/// <summary>
/// Обработчик для команды <see cref="LoginCommand"/>.
/// </summary>
public sealed class LoginHandler(IAuthService authService)
{
    public async Task<LoginResponseDto> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        return await authService.LoginAsync(request.Username, cancellationToken);
    }
}