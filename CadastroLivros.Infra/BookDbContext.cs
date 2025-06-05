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

        public BookDbContext()
        {
        }

        public DbSet<Book> Books { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=localhost,1433;Database=CadastroLivrosDB;User ID=sa;Password=@Abc1234;TrustServerCertificate=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Book>().ToTable("Books");
            modelBuilder.Entity<Book>().HasKey(l => l.Id);
            modelBuilder.Entity<Book>().Property(l => l.Title).IsRequired().HasMaxLength(200);
            modelBuilder.Entity<Book>().Property(l => l.Author).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<Book>().Property(l => l.PublicationDate).IsRequired();
            modelBuilder.Entity<Book>().Property(l => l.Category).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Book>().Property(l => l.Publisher).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<Book>().Property(l => l.ISBN13).IsRequired().HasMaxLength(13);
        }
    }
}