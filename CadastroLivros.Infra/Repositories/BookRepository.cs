using CadastroLivros.Domain.Entities;
using CadastroLivros.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CadastroLivros.Infra.Repositories;

public class BookRepository : IBookRepository
{
    private readonly BookDbContext _context;

    public BookRepository(BookDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Book>> GetAllAsync()
    {
        return await _context.Books.ToListAsync();
    }

    public async Task<Book?> GetByIdAsync(Guid id)
    {
        var book = await _context.Books.FindAsync(id);
        if (book == null)
        {
            throw new KeyNotFoundException("Livro não encontrado.");
        }

        return book;
    }

    public async Task<IEnumerable<Book>> GetByFilterAsync(string? title, string? author, DateTime? publicationDate, string? category)
    {
        return await _context.Books
            .Where(b => (title == null || b.Title.Contains(title)) &&
                        (author == null || b.Author.Contains(author)) &&
                        (publicationDate == null || b.PublicationDate == publicationDate) &&
                        (category == null || b.Category.Contains(category)))
            .ToListAsync();
    }

    public async Task<Book> AddAsync(Book book)
    {
        await _context.Books.AddAsync(book);
        await _context.SaveChangesAsync();
        return book;
    }

    public async Task<Book> UpdateAsync(Book book)
    {
        var existingBook = await _context.Books.FindAsync(book.Id);
        if (existingBook != null)
        {
            _context.Entry(existingBook).CurrentValues.SetValues(book);
            await _context.SaveChangesAsync();
        }
        return book;
    }

    public async Task RemoveAsync(Guid id)
    {
        var book = await _context.Books.FindAsync(id);
        if (book != null)
        {
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
        }
    }
}
