using CadastroLivros.Domain.Interfaces;
using CadastroLivros.Domain.Entities;

namespace CadastroLivros.Application.Services
{
    public class BookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync()
        {
            return await _bookRepository.GetAllAsync();
        }

        public async Task<Book?> GetBookByIdAsync(Guid id)
        {
            return await _bookRepository.GetByIdAsync(id);
        }

        public async Task<Book> AddBookAsync(Book book)
        {
            // Lógica adicional antes de salvar, se necessário
            return await _bookRepository.AddAsync(book);
        }

        public async Task<Book> UpdateBookAsync(Book book)
        {
            // Lógica adicional antes de atualizar, se necessário
            return await _bookRepository.UpdateAsync(book);
        }

        public async Task RemoveBookAsync(Guid id)
        {
            await _bookRepository.RemoveAsync(id);
        }
    }
}