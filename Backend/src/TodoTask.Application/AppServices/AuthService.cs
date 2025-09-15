using TodoTask.Application.AppServices.Abstractions;
using TodoTask.Application.DTOs;

namespace TodoTask.Application.AppServices;

/// <inheritdoc/>
public class AuthService(ITokenService TokenService) : IAuthService
{
    /// <inheritdoc/>
    public Task<LoginResponseDto> LoginAsync(string userName)
    {
        var userId = Guid.NewGuid();
        var token = TokenService.GenerateToken(userId, userName);

        var response = new LoginResponseDto(token);

        return Task.FromResult(response);
    }
}