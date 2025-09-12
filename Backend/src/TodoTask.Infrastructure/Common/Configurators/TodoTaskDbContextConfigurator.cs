using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using TodoTask.GeneralKernel.Database.Configurators;
using TodoTask.Infrastructure.Common.Contexts;

namespace TodoTask.Infrastructure.Common.Configurators;

/// <summary>
/// Конфигуратор контекста базы данных.
/// </summary>
public class TodoTaskDbContextConfigurator(
    IConfiguration configuration,
    ILoggerFactory loggerFactory)
    : BaseDbContextConfigurator<TodoTaskDbContext>(configuration, loggerFactory)
{
    
    /// <inheritdoc />
    protected override string ConnectionStringName => "TodoTaskDbContext";
}