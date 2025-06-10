using CadastroLivros.Application.DTOs;
using CadastroLivros.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class BookController : ControllerBase
{
    private readonly IBookService _iBookService;

    public BookController(IBookService bookService)
    {
        _iBookService = bookService;
    }

    [HttpPost]
    public async Task<IActionResult> AddBook([FromBody] BookDTO bookDTO)
    {
        var bookResponse = await _iBookService.AddBookAsync(bookDTO);
        return CreatedAtAction(nameof(GetBookById), new { id = bookResponse.Id }, bookResponse);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllBooks()
    {
        var books = await _iBookService.GetAllBooksAsync();
        return Ok(books);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetBookById(Guid id)
    {
        var book = await _iBookService.GetBookByIdAsync(id);
        if (book == null)
        {
            return NotFound("Livro não encontrado.");
        }

        return Ok(book);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBook(Guid id, [FromBody] BookDTO bookDTO)
    {
        var updatedBook = await _iBookService.UpdateBookAsync(id, bookDTO);
        return Ok(updatedBook);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> RemoveBook(Guid id)
    {
        await _iBookService.RemoveBookAsync(id);
        return NoContent();
    }
}