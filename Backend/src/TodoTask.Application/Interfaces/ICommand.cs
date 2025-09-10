namespace TodoTask.Application.Interfaces;

/// <summary>
/// Интерфейс-маркер для команд.
/// </summary>
/// <typeparam name="TResponse"></typeparam>
public interface ICommand<TResponse> : IRequest<TResponse> { }
