using Microsoft.EntityFrameworkCore;
using TodoTask.Domain.Aggregates;
using TodoTask.Domain.Entities;

namespace TodoTask.Infrastructure.Common.Contexts;

/// <summary>
/// DbContext для хранение агрегата Issue 
/// </summary>
public class TodoTaskDbContext : DbContext
{
    /// <summary>
    /// Задачи.
    /// </summary>
    public DbSet<Issue> Issues { get; set; }
   
    /// <summary>
    /// Связи между задачами.
    /// </summary>
    public DbSet<RelationIssue> RelationIssues { get; set; }
    
    public TodoTaskDbContext(DbContextOptions<TodoTaskDbContext> options) : base(options) { }
    
    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        CustomModelBuilder.OnModelCreating(modelBuilder);
    }
}