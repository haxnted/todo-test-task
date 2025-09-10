namespace TodoTask.Application.Interfaces;

/// <summary>
/// Интерфейс-маркер для запросов.
/// </summary>
/// <typeparam name="TResponse"></typeparam>
public interface IQuery<TResponse> : IRequest<TResponse> { }
