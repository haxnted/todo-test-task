using TodoTask.Application.DTOs;

namespace TodoTask.Application.AppServices.Abstractions;

/// <summary>
/// Сервис для управления аутентификацией.
/// </summary>
public interface IAuthService
{
    Task<LoginResponseDto> LoginAsync(string userName, CancellationToken cancellationToken);
}