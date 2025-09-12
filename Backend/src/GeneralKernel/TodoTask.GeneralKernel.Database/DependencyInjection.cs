using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TodoTask.GeneralKernel.Database.Abstracts;
using TodoTask.GeneralKernel.Database.Configurations;
using TodoTask.GeneralKernel.Database.Transactions;

namespace TodoTask.GeneralKernel.Database;

/// <summary>
/// Конфигурация зависимостей базы данных.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Добавление зависимостей базы данных.
    /// </summary>
    /// <param name="services">Коллекция сервисов.</param>
    /// <returns></returns>
    public static IServiceCollection AddDatabase<TDbContext, TDbContextConfigurator>(this IServiceCollection services)
        where TDbContext : DbContext
        where TDbContextConfigurator : class, IDbContextOptionsConfigurator<TDbContext>
    {
        services.AddEntityFrameworkNpgsql()
            .AddDbContextPool<TDbContext>(Configure<TDbContext>);

        services
            .AddSingleton<IDbContextOptionsConfigurator<TDbContext>, TDbContextConfigurator>()
            .AddScoped<DbContext>(sp => sp.GetRequiredService<TDbContext>())
            .AddScoped<ITransactionalExecutor, TransactionalExecutor<TDbContext>>()
            .AddScoped(typeof(IRepository<>), typeof(EntityFrameworkRepository<>));

        return services;
    }
    
    internal static void Configure<TDbContext>(IServiceProvider sp, DbContextOptionsBuilder dbOptions) where TDbContext : DbContext
    {
        var configurator = sp.GetRequiredService<IDbContextOptionsConfigurator<TDbContext>>();
        configurator.Configure((DbContextOptionsBuilder<TDbContext>)dbOptions);
    }
    
}