using Microsoft.EntityFrameworkCore;
using TodoTask.Domain.Aggregates;

namespace TodoTask.Infrastructure;

/// <summary>
/// Контекст базы данных.
/// </summary>
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    /// <summary>
    /// Задачи.
    /// </summary>
    public DbSet<Issue> Issues => Set<Issue>();
    
    /// <summary>
    /// Задачи без отслеживания изменений.
    /// </summary>
    public IQueryable<Issue> IssuesAsNoTracking => Issues.AsNoTracking();


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=TodoTask;Username=postgres;Password=postgres");
        
    }
}
