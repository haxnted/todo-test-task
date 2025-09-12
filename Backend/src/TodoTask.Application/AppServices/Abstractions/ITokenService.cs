namespace TodoTask.Application.AppServices.Abstractions;

/// <summary>
/// Интерфейс для генерации JWT токена.
/// </summary>
public interface ITokenService
{
    /// <summary>
    /// Генерирует JWT токен для пользователя.
    /// </summary>
    /// <param name="userId">Идентификатор пользователя.</param>
    /// <param name="username">Имя пользователя.</param>
    /// <returns>JWT токен в виде строки.</returns>
    string GenerateToken(Guid userId, string username);
}