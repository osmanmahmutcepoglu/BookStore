using AutoMapper;
using FluentAssertions;
using System;
using System.Linq;
using WebApi.Application.BookOperations.Commands.CreateBook;
using WebApi.DbOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.BookOperations.Commands.CreateBook
{
    public class CreateBookCommandTests : IClassFixture<CommonTestFixture> 
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateBookCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact] 
        public void Fact_WhenAlreadyExistBookTitleAndAuthorIdAndGenreIdIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            
            var book = new Book() { Title = "WhenAlreadyExistBookTitleAndAuthorIdAndGenreIdIsGiven_InvalidOperationException_ShouldBeReturn", AuthorId = 1, GenreId = 1, PageCount = 100, PublishDate = new System.DateTime(1990, 01, 10) };
            _context.Books.Add(book);
            _context.SaveChanges();

            CreateBookCommand command = new(_context, _mapper);
            command.Model = new CreateBookModel() { Title = book.Title, AuthorId = book.AuthorId, GenreId = book.GenreId };

            
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("The book is already in the system.");
        }

        [Fact] 
        public void Fact_WhenValidInputsAreGiven_Book_ShouldBeCreated()
        {
            
            CreateBookCommand command = new(_context, _mapper);
            command.Model = new CreateBookModel() { Title = "Book", PageCount = 100, PublishDate = DateTime.Now.Date.AddYears(-2), GenreId = 1, AuthorId = 1 };

            
            FluentActions
                .Invoking(() => command.Handle()).Invoke();

            
            var addedBook = _context.Books.FirstOrDefault(x => x.Title == command.Model.Title && x.GenreId == command.Model.GenreId && x.AuthorId == command.Model.AuthorId);
            addedBook.Should().NotBeNull();
            addedBook.PageCount.Should().Be(command.Model.PageCount);
            addedBook.PublishDate.Should().Be(command.Model.PublishDate);
        }
    }
}