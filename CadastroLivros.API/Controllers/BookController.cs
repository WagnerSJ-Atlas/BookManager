using CadastroLivros.Domain.Entities;
using CadastroLivros.Domain.Validators;
using Microsoft.AspNetCore.Mvc;
using CadastroLivros.Application.Services;

namespace CadastroLivros.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly BookValidator _bookValidator;
        private readonly BookService _bookService;
        private readonly ILogger<BookController> _logger;

        public BookController(BookService bookService, ILogger<BookController> logger)
        {
            _bookService = bookService;
            _bookValidator = new BookValidator();
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> AddBook([FromBody] Book book)
        {
            try
            {
                var validationResult = _bookValidator.Validate(book);

                if (!validationResult.IsValid)
                {
                    return BadRequest(validationResult.Errors);
                }

                var bookResponse = await _bookService.AddBookAsync(book);
                return CreatedAtAction(nameof(GetBooks), new { id = bookResponse.Id }, bookResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocorreu um erro ao adicionar o livro: " + ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetBooks()
        {
            try
            {
                var books = await _bookService.GetAllBooksAsync();
                return Ok(books);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocorreu um erro ao recuperar os livros: " + ex.Message);
            }    
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(Guid id, [FromBody] Book book)
        {
            try
            {
                if (id != book.Id)
                {
                    return BadRequest("O ID passado não corresponde ao ID do livro.");
                }

                var existingBook = await _bookService.GetBookByIdAsync(id);
                if (existingBook == null)
                {
                    return NotFound("Livro não encontrado.");
                }

                var validationResult = _bookValidator.Validate(book);

                if (!validationResult.IsValid)
                {
                    return BadRequest(validationResult.Errors);
                }
                existingBook.Title = book.Title;
                existingBook.Author = book.Author;
                existingBook.PublicationDate = book.PublicationDate;
                existingBook.Category = book.Category;
                existingBook.Publisher = book.Publisher;
                existingBook.ISBN13 = book.ISBN13;

                await _bookService.UpdateBookAsync(existingBook);
                return Ok(existingBook);    
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar o livro com ID {Id}", id);
                return StatusCode(500, "Ocorreu um erro ao atualizar o livro: " + ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(Guid id)
        {
            try
            {
                var book = await _bookService.GetBookByIdAsync(id);
                if (book == null)
                {
                    return NotFound("Livro não encontrado.");
                }
                await _bookService.RemoveBookAsync(id);
                return Ok("Livro removido com sucesso.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocorreu um erro ao remover o livro: " + ex.Message);
            }
        }
    }
}