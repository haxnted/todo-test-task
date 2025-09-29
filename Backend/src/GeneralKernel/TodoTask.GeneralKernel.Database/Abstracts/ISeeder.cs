namespace TodoTask.GeneralKernel.Database.Abstracts;

/// <summary>
/// Добавление сущностей в базу данных.
/// </summary>
public interface ISeeder
{
    /// <summary>
    /// Добавляет тестовые данные в базу данных.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    Task SeedAsync(CancellationToken cancellationToken);
}