namespace CadastroLivros.Domain.Entities;

public class Book
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public DateTime PublicationDate { get; set; }
    public string Category { get; set; }
    public string Publisher { get; set; }
    public string ISBN13 { get; set; }

    public Book(string title, string author, DateTime publicationDate, string category, string publisher, string isbn13)
    {
        Id = Guid.NewGuid();
        Title = title;
        Author = author;
        PublicationDate = publicationDate;
        Category = category;
        Publisher = publisher;
        ISBN13 = isbn13;
    }
}
