using AutoMapper;
using WebApi.Application.BookOperations.Commands.CreateBook;
using WebApi.Application.BookOperations.Commands.UpdateBook;
using WebApi.Application.BookOperations.Queries.GetBookDetail;
using WebApi.Application.BookOperations.Queries.GetBooks;
using WebApi.Entities;

namespace WebApi.Mappings
{
    public class BookProfile : Profile 
    {
        public BookProfile()
        {

            CreateMap<CreateBookModel, Book>();

            CreateMap<UpdateBookModel, Book>();
            CreateMap<Book, BookDetailViewModel>()
                .ForMember(destination => destination.Genre, option => option.MapFrom(source => source.Genre.Name))
                .ForMember(destination => destination.AuthorFullName, option => option.MapFrom(source => $"{source.Author.FirstName} {source.Author.LastName}"));
            CreateMap<Book, BooksViewModel>()
                .ForMember(destination => destination.Genre, option => option.MapFrom(source => source.Genre.Name))
                .ForMember(destination => destination.AuthorFullName, option => option.MapFrom(source => $"{source.Author.FirstName} {source.Author.LastName}"));
        }
    }
}