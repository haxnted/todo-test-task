using TodoTask.Domain.Aggregates;
using TodoTask.Domain.Enums;
using TodoTask.GeneralKernel.Database.Abstracts;
using TodoTask.Infrastructure.Common.Contexts;

namespace TodoTask.Infrastructure.Seeders;

/// <summary>
/// Добавляет тестовые задачи и подзадачи в базу данных.
/// </summary>
public sealed class IssueSeeder(TodoTaskDbContext context) : ISeeder
{
    private readonly Random _random = new();

    /// <summary>
    /// Количество главных задач, создаваемых при сидировании.
    /// </summary>
    private const int CountMainIssues = 10;

    /// <summary>
    /// Минимальное количество подзадач на одну главную задачу.
    /// </summary>
    private const int MinSubIssues = 0;

    /// <summary>
    /// Максимальное количество подзадач на одну главную задачу.
    /// </summary>
    private const int MaxSubIssues = 10;

    /// <summary>
    /// Добавляет тестовые задачи в базу данных.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    public async Task SeedAsync(CancellationToken cancellationToken)
    {
        var mainIssues = CreateMainIssues();

        foreach (var mainIssue in mainIssues)
        {
            var countSubIssues = _random.Next(MinSubIssues, MaxSubIssues + 1);

            var subIssues = Enumerable.Range(0, countSubIssues)
                .Select(CreateSubIssue)
                .ToList();

            mainIssue.AddRangeSubIssues(subIssues);
        }

        await context.Issues.AddRangeAsync(mainIssues, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// Создает коллекцию главных задач без подзадач.
    /// </summary>
    private List<Issue> CreateMainIssues() => Enumerable.Range(0, CountMainIssues)
        .Select(CreateMainIssue)
        .ToList();

    /// <summary>
    /// Создает шаблонную главную задачу.
    /// </summary>
    /// <param name="index">Индекс задачи в коллекции.</param>
    private Issue CreateMainIssue(int index) => CreateIssue($"Главная задача {index}", $"Описание к задаче {index}.");

    /// <summary>
    /// Создает шаблонную подзадачу.
    /// </summary>
    /// <param name="index">Индекс подзадачи в коллекции.</param>
    private Issue CreateSubIssue(int index) => CreateIssue($"Подзадача {index}", $"Описание к подзадаче {index}.");

    /// <summary>
    /// Базовый метод для создания задачи или подзадачи.
    /// </summary>
    /// <param name="title">Название задачи.</param>
    /// <param name="description">Описание задачи.</param>
    private static Issue CreateIssue(string title, string description) => Issue.Create(Guid.NewGuid(),
        Guid.NewGuid(),
        IssueStatus.New,
        IssuePriority.Medium,
        null,
        title,
        description);
}