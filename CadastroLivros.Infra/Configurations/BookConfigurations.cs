using CadastroLivros.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class BookConfigurations : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.ToTable("Books");
        builder.HasKey(l => l.Id);
        
        builder.Property(l => l.Id)
            .ValueGeneratedNever()
            .HasColumnType("uniqueidentifier");
        
        builder.Property(l => l.Title).IsRequired().HasMaxLength(200);
        builder.Property(l => l.Author).IsRequired().HasMaxLength(100);
        builder.Property(l => l.PublicationDate).IsRequired();
        builder.Property(l => l.Category).IsRequired().HasMaxLength(50);
        builder.Property(l => l.Publisher).IsRequired().HasMaxLength(100);
        builder.Property(l => l.ISBN13).IsRequired().HasMaxLength(13);
    }
}