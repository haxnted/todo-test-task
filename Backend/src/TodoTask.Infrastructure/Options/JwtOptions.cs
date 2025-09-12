namespace TodoTask.Infrastructure.Options;

/// <summary>
/// Настройки JWT.
/// </summary>
public class JwtOptions
{
    public string Secret { get; set; } = null!;
    public int ExpiryMinutes { get; set; }
}