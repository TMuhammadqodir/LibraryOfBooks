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
}
