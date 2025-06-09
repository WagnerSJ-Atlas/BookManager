using CadastroLivros.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CadastroLivros.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly BookService _bookService;
        private readonly ILogger<BookController> _logger;

        public BookController(BookService bookService, ILogger<BookController> logger)
        {
            _bookService = bookService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> AddBook([FromBody] Book book)
        {
            try
            {
                var bookResponse = await _bookService.AddBookAsync(book);
                return CreatedAtAction(nameof(GetBooks), new { id = bookResponse.Id }, bookResponse);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao adicionar o livro.");
                return StatusCode(500, "Ocorreu um erro ao adicionar o livro.");
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
                _logger.LogError(ex, "Erro ao recuperar os livros.");
                return StatusCode(500, "Ocorreu um erro ao recuperar os livros.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(Guid id, [FromBody] Book book)
        {
            try
            {
                var updatedBook = await _bookService.UpdateBookAsync(id, book);
                return Ok(updatedBook);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Livro não encontrado.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar o livro com ID {Id}", id);
                return StatusCode(500, "Ocorreu um erro ao atualizar o livro.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(Guid id)
        {
            try
            {
                await _bookService.RemoveBookAsync(id);
                return Ok("Livro removido com sucesso.");
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Livro não encontrado.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao remover o livro com ID {Id}", id);
                return StatusCode(500, "Ocorreu um erro ao remover o livro.");
            }
        }
    }
}