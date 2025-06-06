using Microsoft.EntityFrameworkCore;
using CadastroLivros.Domain.Entities;

namespace CadastroLivros.Infra
{
    public class BookDbContext : DbContext
    {
        public BookDbContext(DbContextOptions<BookDbContext> options)
            : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BookConfigurations());
            base.OnModelCreating(modelBuilder);
        }
    }
}