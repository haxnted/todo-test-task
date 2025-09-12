using Microsoft.EntityFrameworkCore;
using TodoTask.GeneralKernel.Database.Configurations;
using TodoTask.Infrastructure.Common.Contexts.Configurations;

namespace TodoTask.Infrastructure.Common.Contexts
{
    /// <summary>
    /// Сборщик моделей.
    /// </summary>
    public static class CustomModelBuilder
    {
        /// <summary>
        /// Конфигурирует модель EF Core для микросервиса обработки контрактов выгрузки и инфраструктуры.
        /// </summary>
        /// <param name="modelBuilder">Конфигуратор модели EF Core.</param>
        public static void OnModelCreating(ModelBuilder modelBuilder)
        {

            // Применяем конфигурации доменных агрегатов
            modelBuilder.ApplyConfiguration(new IssueConfiguration());
            modelBuilder.ApplyConfiguration(new RelationIssueConfiguration());

            // Настройка типов DateTime по умолчанию в UTC
            modelBuilder.SetDefaultDateTimeKind(DateTimeKind.Utc);
        }
    }
}
