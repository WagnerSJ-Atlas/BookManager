using CadastroLivros.Application.DTOs;
using CadastroLivros.Application.Views;

namespace CadastroLivros.Application.Interfaces;

public interface IBookService
{
    Task<BookView> AddBookAsync(BookDTO bookDTO);
    Task<IEnumerable<BookView>> GetAllBooksAsync();
    Task<IEnumerable<BookView>> GetBooksByFilterAsync(string? title, string? author, DateTime? publicationDate, string? category);
    Task<BookView?> GetBookByIdAsync(Guid id);
    Task<BookView> UpdateBookAsync(Guid id, BookDTO bookDTO);
    Task RemoveBookAsync(Guid id);
}