using Microsoft.EntityFrameworkCore;
using MyBookWorld.Models;

namespace MyBookWorld.data;
public class ApplicationDBcontext : DbContext
{
    public ApplicationDBcontext(DbContextOptions<ApplicationDBcontext> options) : base(options) { }
    public DbSet<Author> Authors { get; set; }  // Author table
    public DbSet<Book> Books { get; set; }   // Books table
    public DbSet<Category> Categories { get; set; }   // Categories table
    public DbSet<Quote> Quotes {get; set;} // Quotes table
}