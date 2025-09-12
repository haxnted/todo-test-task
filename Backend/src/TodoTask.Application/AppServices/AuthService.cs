using TodoTask.Application.AppServices.Abstractions;
using TodoTask.Application.DTOs;

namespace TodoTask.Application.AppServices;

/// <inheritdoc/>
public class AuthService : IAuthService
{
    private readonly ITokenService _tokenService;

    public AuthService(ITokenService tokenService)
    {
        _tokenService = tokenService;
    }

    /// <inheritdoc/>
    public Task<LoginResponseDto> LoginAsync(string userName, CancellationToken cancellationToken)
    {
        var userId = Guid.NewGuid();
        var token = _tokenService.GenerateToken(userId, userName);

        var response = new LoginResponseDto()
        {
            Token = token
        };

        return Task.FromResult(response);
    }
}