using System.Linq.Expressions;
using Ardalis.Specification;

namespace TodoTask.GeneralKernel.Database.Abstracts
{
    /// <summary>
    /// Репозиторий для доступа к данным.
    /// </summary>
    /// <typeparam name="TEntity">Тип сущности.</typeparam>
    public interface IRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Добавляет сущность в хранилище.
        /// </summary>
        /// <param name="entity">Сущность.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        Task AddAsync(TEntity entity, CancellationToken cancellationToken);

        /// <summary>
        /// Добавляет список сущностей в хранилище.
        /// </summary>
        /// <param name="entities">Сущности.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        Task AddRangeAsync(TEntity[] entities, CancellationToken cancellationToken);

        /// <summary>
        /// Выполняет запрос с использованием спецификации.
        /// </summary>
        /// <typeparam name="TSpec">Тип спецификации.</typeparam>
        IQueryable<TEntity> WithSpecification<TSpec>(TSpec specification) where TSpec : ISpecification<TEntity>;
        
        /// <summary>
        /// Обновляет данные сущности.
        /// </summary>
        /// <param name="entity">Сущность.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        Task UpdateAsync(TEntity entity, CancellationToken cancellationToken);
        
        /// <summary>
        /// Удаляет сущность из хранилища.
        /// </summary>
        /// <param name="entity">Сущность.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        Task RemoveAsync(TEntity entity, CancellationToken cancellationToken);
        
    }
}
