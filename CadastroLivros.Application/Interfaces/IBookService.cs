using CadastroLivros.Domain.Entities;

public interface IBookService
{
    Task<IEnumerable<Book>> GetAllBooksAsync();
    Task<Book?> GetBookByIdAsync(Guid id);
    Task<Book> AddBookAsync(Book book);
    Task<Book> UpdateBookAsync(Guid id, Book book);
    Task RemoveBookAsync(Guid id);
}