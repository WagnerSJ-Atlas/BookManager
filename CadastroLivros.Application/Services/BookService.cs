using CadastroLivros.Application.DTOs;
using CadastroLivros.Application.Views;
using CadastroLivros.Application.Interfaces;
using CadastroLivros.Domain.Entities;
using CadastroLivros.Domain.Interfaces;
using CadastroLivros.Domain.Validators;
using Mapster;

public class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;
    private readonly BookValidator _bookValidator;

    public BookService(IBookRepository bookRepository, BookValidator bookValidator)
    {
        _bookRepository = bookRepository;
        _bookValidator = bookValidator;
    }

    public async Task<BookView> AddBookAsync(BookDTO bookDTO)
    {
        var book = bookDTO.Adapt<Book>(); // Converte DTO para Entidade

        var validationResult = _bookValidator.Validate(book);
        if (!validationResult.IsValid)
        {
            throw new ArgumentException("Erro de validação: " + string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage)));
        }

        var createdBook = await _bookRepository.AddAsync(book);
        return createdBook.Adapt<BookView>(); // Converte Entidade para View
    }

    public async Task<IEnumerable<BookView>> GetAllBooksAsync()
    {
        var books = await _bookRepository.GetAllAsync();
        return books.Adapt<IEnumerable<BookView>>(); // Converte Entidades para Views
    }

    public async Task<BookView?> GetBookByIdAsync(Guid id)
    {
        var book = await _bookRepository.GetByIdAsync(id);
        return book?.Adapt<BookView>(); // Converte Entidade para View
    }

    public async Task<IEnumerable<BookView>> GetBooksByFilterAsync(string? title, string? author, DateTime? publicationDate, string? category)
    {
        var books = await _bookRepository.GetByFilterAsync(title, author, publicationDate, category);
        return books.Adapt<IEnumerable<BookView>>(); // Converte Entidades para Views
    }

    public async Task<BookView> UpdateBookAsync(Guid id, BookDTO bookDTO)
    {
        var existingBook = await _bookRepository.GetByIdAsync(id);
        if (existingBook == null)
        {
            throw new KeyNotFoundException("Livro não encontrado.");
        }

        // Atualiza os valores da entidade com os dados do DTO
        existingBook.Title = bookDTO.Title;
        existingBook.Author = bookDTO.Author;
        existingBook.PublicationDate = bookDTO.PublicationDate;
        existingBook.Category = bookDTO.Category;
        existingBook.Publisher = bookDTO.Publisher;
        existingBook.ISBN13 = bookDTO.ISBN13;

        var validationResult = _bookValidator.Validate(existingBook);
        if (!validationResult.IsValid)
        {
            throw new ArgumentException("Erro de validação: " + string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage)));
        }

        var updatedBook = await _bookRepository.UpdateAsync(existingBook);
        return updatedBook.Adapt<BookView>(); // Converte Entidade para View
    }

    public async Task RemoveBookAsync(Guid id)
    {
        await _bookRepository.RemoveAsync(id);
    }
}