using Microsoft.EntityFrameworkCore;
using TweetService.Domain.Models;

namespace TweetService.Infrastructure;

public class ApplicationContext(DbContextOptions<ApplicationContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationContext).Assembly);
    }
    public DbSet<Tweet> Tweets { get; set; }
}