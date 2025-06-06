using CadastroLivros.Domain.Entities;
using CadastroLivros.Domain.Interfaces;
using CadastroLivros.Domain.Validators;
using Microsoft.AspNetCore.Mvc;

namespace CadastroLivros.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly BookValidator _bookValidator;
        private readonly IBookRepository _bookRepository;

        public BookController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
            _bookValidator = new BookValidator();
        }

        [HttpPost]
        public async Task<IActionResult> AddBook([FromBody] Book book)
        {
            var validator = new BookValidator();
            var validationResult = validator.Validate(book);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var bookResponse = await _bookRepository.AddAsync(book);
            return CreatedAtAction(nameof(GetBooks), new { id = bookResponse.Id }, bookResponse);
        }

        [HttpGet]
        public async Task<IActionResult> GetBooks()
        {
            var books = await _bookRepository.GetAllAsync();
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(Guid id, [FromBody] Book book)
        {
            if (id != book.Id)
            {
                return BadRequest("O ID passado não corresponde ao ID do livro.");
            }

            var existingBook = await _bookRepository.GetByIdAsync(id);
            if (existingBook == null)
            {
                return NotFound("Livro não encontrado.");
            }

            var validationResult = _bookValidator.Validate(book);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            // Atualizar os valores do livro existente
            existingBook.Title = book.Title;
            existingBook.Author = book.Author;
            existingBook.PublicationDate = book.PublicationDate;
            existingBook.Category = book.Category;
            existingBook.Publisher = book.Publisher;
            existingBook.ISBN13 = book.ISBN13;

            await _bookRepository.UpdateAsync(existingBook);
            return Ok("Livro atualizado com sucesso.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(Guid id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null)
            {
                return NotFound("Livro não encontrado.");
            }
            await _bookRepository.RemoveAsync(id);
            return Ok("Livro removido com sucesso.");
        }

    }
}