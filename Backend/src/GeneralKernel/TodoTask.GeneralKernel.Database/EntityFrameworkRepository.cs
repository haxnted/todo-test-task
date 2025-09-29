using System.Linq.Expressions;
using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TodoTask.GeneralKernel.Database.Abstracts;

namespace TodoTask.GeneralKernel.Database
{
    /// <inheritdoc/>
    public class EntityFrameworkRepository<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
        /// <summary>
        /// Контекст базы данных.
        /// </summary>
        protected DbContext DbContext { get; }

        /// <summary>
        /// Хранилище сущностей <typeparamref name="TEntity"/>.
        /// </summary>
        protected DbSet<TEntity> DbSet { get; }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="context">Контекст базы данных.</param>
        public EntityFrameworkRepository(DbContext context)
        {
            DbContext = context;
            DbSet = DbContext.Set<TEntity>();
        }

        /// <inheritdoc/>
        public async Task AddAsync(TEntity entity, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(entity);

            await DbContext.AddAsync(entity, cancellationToken);

            await SaveChangesAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public async Task AddRangeAsync(TEntity[] entities, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(entities);

            await DbContext.AddRangeAsync(entities, cancellationToken);

            await SaveChangesAsync(cancellationToken);
        }

        public Task<TEntity?> FirstOrDefaultAsync<TSpec>(TSpec specification, CancellationToken cancellationToken)
            where TSpec : ISpecification<TEntity>
        {
            ArgumentNullException.ThrowIfNull(specification);

            return DbSet.WithSpecification(specification)
                .FirstOrDefaultAsync(cancellationToken);
        }

        /// <inheritdoc />
        public async Task<IReadOnlyList<TEntity>> GetAll<TSpec>(TSpec specification, CancellationToken cancellationToken)
            where TSpec : ISpecification<TEntity>
        {
            ArgumentNullException.ThrowIfNull(specification);

            return await DbSet.WithSpecification(specification)
                .ToListAsync(cancellationToken);
        }

        /// <inheritdoc />
        public Task UpdateAsync(TEntity entity, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(entity);

            var state = DbContext.Entry(entity)
                .State;

            if (state == EntityState.Detached)
            {
                DbContext.Attach(entity);
            }

            DbContext.Update(entity);

            return SaveChangesAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public Task RemoveAsync(TEntity entity, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(entity);
            DbSet.Remove(entity);

            return SaveChangesAsync(cancellationToken);
        }

        /// <inheritdoc cref="RemoveRangeAsync" />
        public Task RemoveRangeAsync(TEntity[] entities, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(entities);
            DbSet.RemoveRange(entities);

            return SaveChangesAsync(cancellationToken);
        }

        /// <inheritdoc cref="SaveChangesAsync" />
        private Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return DbContext.SaveChangesAsync(cancellationToken);
        }
    }
}