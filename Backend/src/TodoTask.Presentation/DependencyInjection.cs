namespace TodoTask.Presentation;

/// <summary>
/// Конфигурация зависимостей приложения
/// </summary>
public static class DependencyInjection
{
    
    /// <summary>
    /// Конфигурация сервисов
    /// </summary>
    /// <param name="services">Коллекция сервисов</param>
    public static IServiceCollection ConfigureServices(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        return services;
    }

    /// <summary>
    /// Конфигурация конвеера приложения
    /// </summary>
    /// <param name="app">Веб-приложение</param>
    public static void ConfigurePipeline(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
    }
}
