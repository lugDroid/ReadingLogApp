using Microsoft.EntityFrameworkCore;
using ReadingLog.Core;

namespace ReadingLog.Data
{
    public class ReadingLogDbContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }

        public ReadingLogDbContext(DbContextOptions<ReadingLogDbContext> options) : base(options)
        {

        }
    }
}