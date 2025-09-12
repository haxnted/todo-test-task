namespace TodoTask.Application.Handlers.Auth.Commands.Login;

/// <summary>
/// Команда на вход в систему.
/// </summary>
/// <param name="Username">Имя пользователя.</param>
/// <param name="Password"></param>
public sealed record LoginCommand(string Username);