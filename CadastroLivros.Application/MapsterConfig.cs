using CadastroLivros.Application.DTOs;
using CadastroLivros.Application.Views;
using CadastroLivros.Domain.Entities;
using Mapster;

namespace CadastroLivros.Application.Mappings;

public static class MapsterConfig
{
    public static void RegisterMappings()
    {
        TypeAdapterConfig<BookDTO, Book>.NewConfig();

        TypeAdapterConfig<Book, BookView>.NewConfig();
    }
}