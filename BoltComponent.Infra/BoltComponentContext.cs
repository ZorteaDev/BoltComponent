using System.ComponentModel;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace BoltComponent.Infra;

public class BoltComponentContext(DbContextOptions<BoltComponentContext> options) : DbContext(options)
{
    public DbSet<Component> Components { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}