namespace CadastroLivros.Application.Views;

public class BookView
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
}