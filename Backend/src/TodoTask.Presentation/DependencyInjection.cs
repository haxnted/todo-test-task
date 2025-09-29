using System.Reflection;
using Elastic.CommonSchema.Serilog;
using Elastic.Ingest.Elasticsearch;
using Elastic.Ingest.Elasticsearch.DataStreams;
using Elastic.Serilog.Sinks;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Events;
using TodoTask.Application.AppServices;
using TodoTask.Application.AppServices.Abstractions;
using TodoTask.Application.Handlers.Issues.Commands.CreateIssue;
using TodoTask.GeneralKernel.Database;
using TodoTask.GeneralKernel.Database.Abstracts;
using TodoTask.Infrastructure.Auth;
using TodoTask.Infrastructure.Common.Configurators;
using TodoTask.Infrastructure.Common.Contexts;
using TodoTask.Infrastructure.Options;
using TodoTask.Infrastructure.Seeders;
using TodoTask.Presentation.Middlewares;
using Wolverine;

namespace TodoTask.Presentation;

/// <summary>
/// Конфигурация зависимостей приложения.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Внедрение всех зависимостей.
    /// </summary>
    /// <param name="services">Коллекция сервисов.</param>
    /// <param name="configuration">Конфигурация.</param>
    public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddApplicationServices();
        services.RegisterDatabases();

        services.AddWolverine(opts =>
        {
            opts.Discovery.IncludeAssembly(typeof(CreateIssueHandler).Assembly);
        });

        services.Configure<JwtOptions>(configuration.GetSection("JwtSettings"));

        services.AddSingleton(resolver =>
            resolver.GetRequiredService<Microsoft.Extensions.Options.IOptions<JwtOptions>>()
                .Value);

        services.ConfigureLogging();

        services.ConfigureSeeders();
    }

    /// <summary>
    /// Конфигурирует добавление тестовых данных в базу данных.
    /// </summary>
    /// <param name="services">Коллекция сервисов.</param>
    public static void ConfigureSeeders(this IServiceCollection services)
    {
        services.AddScoped<ISeeder, IssueSeeder>();
    }
    
    /// <summary>
    /// Конфигурирует логгер.
    /// </summary>
    /// <param name="services">Коллекция сервисов.</param>
    public static void ConfigureLogging(this IServiceCollection services)
    {
        var assemblyName = Assembly.GetExecutingAssembly()
                               .GetName()
                               .Name
                           ?? "app";

        var indexFormat = $"{assemblyName.ToLower().Replace(".", "-")}-{DateTime.UtcNow:yyyy-MM-dd}";

        Log.Logger = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .WriteTo.Elasticsearch([new Uri("http://elasticsearch:9200")], options =>
            {
                options.DataStream = new(indexFormat);
                options.TextFormatting = new();
                options.BootstrapMethod = BootstrapMethod.Silent;
            })
            .MinimumLevel.Override("Microsoft.AspNetCore.Hosting", LogEventLevel.Warning)
            .MinimumLevel.Override("Microsoft.AspNetCore.Mvc", LogEventLevel.Warning)
            .MinimumLevel.Override("Microsoft.AspNetCore.Routing", LogEventLevel.Warning)
            .CreateLogger();

        services.AddSerilog();
    }

    /// <summary>
    /// Добавление поля авторизации в Swagger.
    /// </summary>
    /// <param name="services">Коллекция сервисов.</param>
    private static IServiceCollection AddAuthFieldInSwagger(this IServiceCollection services)
    {
        return services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new()
            {
                Title = "My API",
                Version = "v1"
            });

            c.AddSecurityDefinition("Bearer",
                new()
                {
                    In = ParameterLocation.Header,
                    Description = "Вставьте ваш токен в поле Authorization",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });

            c.AddSecurityRequirement(new()
            {
                {
                    new()
                    {
                        Reference = new()
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    []
                }
            });
        });
    }

    /// <summary>
    /// Регистрация баз данных.
    /// </summary>
    /// <param name="services">Список сервисов.</param>
    private static void RegisterDatabases(this IServiceCollection services)
    {
        services.AddDatabase<TodoTaskDbContext, TodoTaskDbContextConfigurator>();
    }

    /// <summary>
    /// Регистрация сервисов.
    /// </summary>
    /// <param name="services">Список сервисов</param>
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IIssueService, IssueService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ITokenService, JwtTokenService>();
    }

    /// <summary>
    /// Конфигурация конвеера приложения.
    /// </summary>
    /// <param name="app">Веб-приложение.</param>
    public static void ConfigurePipeline(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseMiddleware<ErrorMiddleware>();
        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
    }
}