using Microsoft.EntityFrameworkCore;

namespace DbContextExperiment;

public sealed class DbExperimentContext : DbContext
{
    public DbSet<User> Users => Set<User>();
    public DbExperimentContext() => Database.EnsureCreated();
 
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=expapp.db");
    }
}