using Microsoft.EntityFrameworkCore;
using MusicLibraryAPI.Entities;

namespace MusicLibraryAPI.Data;

public class LibraryContext : DbContext
{
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Song> Songs { get; set; }
    public DbSet<User> Users { get; set; }

    public LibraryContext(DbContextOptions options): base(options) { }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSnakeCaseNamingConvention();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<User>()
            .HasMany(u => u.UserSongs)
            .WithOne(x => x.User);
        
        
    }
}