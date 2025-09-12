namespace TodoTask.Application.DTOs;

/// <summary>
/// Ответ на вход в систему.
/// </summary>
public class LoginResponseDto
{
    /// <summary>
    /// Токен.
    /// </summary>
    public string Token { get; set; }
}