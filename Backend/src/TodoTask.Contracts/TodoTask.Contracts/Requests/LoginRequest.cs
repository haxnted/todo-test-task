namespace TodoTask.Contracts.Requests;

/// <summary>
/// Запрос на вход в систему.
/// </summary>
public class LoginRequest
{
    /// <summary>
    /// Имя пользователя.
    /// </summary>
    public string UserName { get; set; }
}