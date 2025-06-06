using CadastroLivros.Domain.Entities;

namespace CadastroLivros.Domain.Interfaces;

public interface IBookRepository
{
    Task<IEnumerable<Book>> GetAllAsync();
    Task<Book?> GetByIdAsync(Guid id);
    Task<IEnumerable<Book>> GetByFilterAsync(string? title, string? author, DateTime? publicationDate, string? category);
    Task<Book> AddAsync(Book book);
    Task<Book> UpdateAsync(Book book);
    Task RemoveAsync(Guid id);
}
