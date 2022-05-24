using CommanderGQL.Models;
using Microsoft.EntityFrameworkCore;

namespace CommanderGQL.Data;

public class AppDbContext : DbContext
{
    public DbSet<Platform> Platforms { get; set; }
    public DbSet<Command> Commands { get; set; }

    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Platform>()
                    .HasMany(p => p.Commands)
                    .WithOne(command => command.Platform)
                    .HasForeignKey(command => command.PlatformId);

        modelBuilder.Entity<Command>()
                    .HasOne(command => command.Platform)
                    .WithMany(platform => platform.Commands)
                    .HasForeignKey(p => p.PlatformId);

        base.OnModelCreating(modelBuilder);
    }
}