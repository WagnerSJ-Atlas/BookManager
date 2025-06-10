using CadastroLivros.Domain.Entities;
using CadastroLivros.Domain.Validators;
using CadastroLivros.Domain.Interfaces;

public class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;
    private readonly BookValidator _bookValidator;

    public BookService(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
        _bookValidator = new BookValidator();
    }

    public async Task<Book> AddBookAsync(Book book)
    {
        var validationResult = _bookValidator.Validate(book);
        if (!validationResult.IsValid)
        {
            throw new ArgumentException("Erro de validação: " + string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage)));
        }

        return await _bookRepository.AddAsync(book);
    }

    public async Task<Book> UpdateBookAsync(Guid id, Book book)
    {
        if (id != book.Id)
        {
            throw new ArgumentException("O ID passado não corresponde ao ID do livro.");
        }

        var existingBook = await _bookRepository.GetByIdAsync(id);
        if (existingBook == null)
        {
            throw new KeyNotFoundException("Livro não encontrado.");
        }

        var validationResult = _bookValidator.Validate(book);
        if (!validationResult.IsValid)
        {
            throw new ArgumentException("Erro de validação: " + string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage)));
        }

        existingBook.Title = book.Title;
        existingBook.Author = book.Author;
        existingBook.PublicationDate = book.PublicationDate;
        existingBook.Category = book.Category;
        existingBook.Publisher = book.Publisher;
        existingBook.ISBN13 = book.ISBN13;

        return await _bookRepository.UpdateAsync(existingBook);
    }

    public async Task RemoveBookAsync(Guid id)
    {
        var book = await _bookRepository.GetByIdAsync(id);
        if (book == null)
        {
            throw new KeyNotFoundException("Livro não encontrado.");
        }

        await _bookRepository.RemoveAsync(id);
    }

    public async Task<IEnumerable<Book>> GetAllBooksAsync()
    {
        return await _bookRepository.GetAllAsync();
    }

    public async Task<Book?> GetBookByIdAsync(Guid id)
    {
        var book = await _bookRepository.GetByIdAsync(id);
        if (book == null)
        {
            throw new KeyNotFoundException("Livro não encontrado.");
        }

        return book;
    }
}