using ServiceExchange.Core.CategoryAggregate;
using System.Reflection;
using Ardalis.SharedKernel;
using Microsoft.EntityFrameworkCore;

namespace ServiceExchange.Infrastructure.Data;

using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    
    public DbSet<Category> Categories => Set<Category>();
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ServiceExchangeDb;Integrated Security=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
    }
}