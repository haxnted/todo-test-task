namespace TodoTask.Infrastructure.Options;

/// <summary>
/// Настройки JWT.
/// </summary>
public class JwtOptions
{
    /// <summary>
    /// Секретный ключ.
    /// </summary>
    public string Secret { get; init; } = null!;

    /// <summary>
    /// Срок действия токена.
    /// </summary>
    public int ExpiryMinutes { get; init; }
}