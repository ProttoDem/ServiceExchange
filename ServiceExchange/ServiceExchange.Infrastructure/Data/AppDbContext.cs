using ServiceExchange.Core.CategoryAggregate;
using System.Reflection;
using Ardalis.SharedKernel;
using Microsoft.EntityFrameworkCore;
using ServiceExchange.Core.CalendarAggregate;
using ServiceExchange.Core.ReportAggregate;
using ServiceExchange.Core.RoleAggregate;
using ServiceExchange.Core.UserAggregate;
using Task = ServiceExchange.Core.TaskAggregate.Task;

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
    public DbSet<Report> Reports => Set<Report>(); 
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            "Server=ssw-wsa-0221;Initial Catalog=ServiceExchangeDb;User id=sa;Password=Formpipe2008;Integrated Security=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;TrustServerCertificate=True;"); 
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        /*modelBuilder.Entity<Task>()
            .HasOne(e => e.Calendar)
            .WithOne(e => e.Task)
            .HasForeignKey<Calendar>(e => e.TaskId)
            .IsRequired();*/
        
        modelBuilder.Entity<Category>()
            .HasMany(e => e.Tasks)
            .WithOne(e => e.Category)
            .HasForeignKey(e => e.CategoryId)
            .IsRequired();
        
        base.OnModelCreating(modelBuilder);
    }
}