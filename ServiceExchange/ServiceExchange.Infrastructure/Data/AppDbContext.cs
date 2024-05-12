using ServiceExchange.Core.CategoryAggregate;
using System.Reflection;
using Ardalis.SharedKernel;
using Microsoft.EntityFrameworkCore;
using ServiceExchange.Core.CalendarAggregate;
using ServiceExchange.Core.RoleAggregate;
using ServiceExchange.Core.UserAggregate;

namespace ServiceExchange.Infrastructure.Data;

using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Core.TaskAggregate.Task> Tasks => Set<Core.TaskAggregate.Task>();
    public DbSet<Calendar> Calendars => Set<Calendar>();
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ServiceExchangeDb;Integrated Security=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }
}