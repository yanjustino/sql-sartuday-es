using Domain.Models;
using Infrastructure.Database.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Database;

public class SqlSartudayDbContext : DbContext
{
    public DbSet<Position> Positions { get; set; }
    public DbSet<Movement> Movements { get; set; }

    public SqlSartudayDbContext(DbContextOptions<SqlSartudayDbContext> options)
    :base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.EnableDetailedErrors();
        optionsBuilder.EnableSensitiveDataLogging();
        optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new MovementConfiguration());
        modelBuilder.ApplyConfiguration(new PositionConfiguration());
        base.OnModelCreating(modelBuilder);
    }
}
