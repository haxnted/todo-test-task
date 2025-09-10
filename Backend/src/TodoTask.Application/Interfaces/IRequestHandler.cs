namespace TodoTask.Application.Interfaces;

/// <summary>
/// Описывает обработчик запроса.
/// </summary>
/// <typeparam name="TRequest">Запрос.</typeparam>
/// <typeparam name="TResponse"></typeparam>
public interface IRequestHandler<in TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken); 
}
