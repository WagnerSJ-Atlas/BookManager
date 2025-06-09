using System.ComponentModel.DataAnnotations;

namespace CadastroLivros.Domain.Entities;

public class Book
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public DateTime PublicationDate { get; set; }
    public string Category { get; set; } = string.Empty;
    public string Publisher { get; set; } = string.Empty;
    public string ISBN13 { get; set; } = string.Empty;
}
