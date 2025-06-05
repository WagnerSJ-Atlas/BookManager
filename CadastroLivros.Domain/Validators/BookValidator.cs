using FluentValidation;
using CadastroLivros.Domain.Entities;

namespace CadastroLivros.Domain.Validators
{
    public class BookValidator : AbstractValidator<Book>
    {
        public BookValidator()
        {
            RuleFor(book => book.Title)
                .NotEmpty().WithMessage("O título é obrigatório.");

            RuleFor(book => book.Author)
                .NotEmpty().WithMessage("O autor é obrigatório.");

            RuleFor(book => book.Category)
                .NotEmpty().WithMessage("A categoria é obrigatória.");

            RuleFor(book => book.PublicationDate)
                .NotEmpty().WithMessage("A data de publicação é obrigatória.");

            RuleFor(book => book.Publisher)
                .NotEmpty().WithMessage("A editora é obrigatória.");

            RuleFor(book => book.ISBN13)
                .NotEmpty().WithMessage("O ISBN13 é obrigatório.")
                .Must(IsValidISBN13).WithMessage("O ISBN13 é inválido.");
        }

        private bool IsValidISBN13(string isbn13)
        {
            if (isbn13.Length != 13 || !isbn13.All(char.IsDigit))
            {
                return false;
            }

            int sum = 0;
            for (int i = 0; i < 12; i++)
            {
                int digit = int.Parse(isbn13[i].ToString());
                sum += (i % 2 == 0) ? digit : digit * 3;
            }

            int checkDigit = (10 - (sum % 10)) % 10;
            return checkDigit == int.Parse(isbn13[12].ToString());
        }
    }
}