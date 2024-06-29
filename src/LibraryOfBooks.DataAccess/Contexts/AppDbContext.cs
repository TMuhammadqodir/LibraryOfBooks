using LibraryOfBooks.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryOfBooks.Dataccess.Contexts;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {}
    public DbSet<Book> Books { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Asset> Assets { get; set; }
    public DbSet<Favorite> Favorites { get; set; }
    public DbSet<BookCategory> BookCategories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        #region Filter
        modelBuilder.Entity<Book>().HasQueryFilter(u => !u.IsDeleted);
        modelBuilder.Entity<User>().HasQueryFilter(u => !u.IsDeleted);
        modelBuilder.Entity<Asset>().HasQueryFilter(u => !u.IsDeleted);
        modelBuilder.Entity<Favorite>().HasQueryFilter(u => !u.IsDeleted);
        modelBuilder.Entity<BookCategory>().HasQueryFilter(u => !u.IsDeleted);
        #endregion

        #region Seed Data
        modelBuilder.Entity<BookCategory>().HasData(
             new BookCategory { Id = 1, Name = "Adabiyot", CreatedAt = DateTime.UtcNow, UpdatedAt = null },
             new BookCategory { Id = 2, Name = "Ilmiy-fantastika", CreatedAt = DateTime.UtcNow, UpdatedAt = null },
             new BookCategory { Id = 3, Name = "Fantaziya", CreatedAt = DateTime.UtcNow, UpdatedAt = null },
             new BookCategory { Id = 4, Name = "Detektiv va Triller", CreatedAt = DateTime.UtcNow, UpdatedAt = null },
             new BookCategory { Id = 5, Name = "Romantika", CreatedAt = DateTime.UtcNow, UpdatedAt = null },
             new BookCategory { Id = 6, Name = "Ilmiy", CreatedAt = DateTime.UtcNow, UpdatedAt = null },
             new BookCategory { Id = 7, Name = "Biznes va Iqtisodiyot", CreatedAt = DateTime.UtcNow, UpdatedAt = null },
             new BookCategory { Id = 8, Name = "O'z-o'zini rivojlantirish", CreatedAt = DateTime.UtcNow, UpdatedAt = null },
             new BookCategory { Id = 9, Name = "Tarix", CreatedAt = DateTime.UtcNow, UpdatedAt = null },
             new BookCategory { Id = 10, Name = "Bolalar adabiyoti", CreatedAt = DateTime.UtcNow, UpdatedAt = null },
             new BookCategory { Id = 11, Name = "San'at va Madaniyat", CreatedAt = DateTime.UtcNow, UpdatedAt = null },
             new BookCategory { Id = 12, Name = "Bolalar adabiyoti", CreatedAt = DateTime.UtcNow, UpdatedAt = null }
             );
        #endregion
    }
}
