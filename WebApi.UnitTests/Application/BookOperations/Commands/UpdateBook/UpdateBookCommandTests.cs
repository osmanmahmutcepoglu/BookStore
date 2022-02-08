using AutoMapper;
using FluentAssertions;
using System;
using System.Linq;
using WebApi.Application.BookOperations.Commands.UpdateBook;
using WebApi.DbOperations;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommandTests : IClassFixture<CommonTestFixture> 
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public UpdateBookCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Theory]

        [InlineData(7)]
        [InlineData(8)]
        [InlineData(9)]
        [InlineData(10)]
        public void Theory_WhenNotExistedBookIdIsGiven_InvalidOperationException_ShouldBeReturn(int bookId)
        {

            UpdateBookCommand command = new(_context, _mapper);
            command.BookId = bookId;
            command.Model = new UpdateBookModel() { Title = "Book", AuthorId = 2, GenreId = 2, PageCount = 106, PublishDate = new DateTime(1990, 01, 01) };

            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("No book found to be updated!");
        }

        [Fact]
        public void Fact_WhenValidInputsAreGiven_Book_ShouldBeUpdated()
        {

            UpdateBookCommand command = new(_context, _mapper);
            command.BookId = 1;
            command.Model = new UpdateBookModel() { Title = "Book", AuthorId = 2, GenreId = 2, PageCount = 800 };


            FluentActions
                .Invoking(() => command.Handle()).Invoke();

            var updatedBook = _context.Books.FirstOrDefault(x => x.Id == command.BookId);
            updatedBook.Should().NotBeNull();
            updatedBook.Title.Should().Be(command.Model.Title);
            updatedBook.AuthorId.Should().Be(command.Model.AuthorId);
            updatedBook.GenreId.Should().Be(command.Model.GenreId);
            updatedBook.PageCount.Should().Be(command.Model.PageCount);
            updatedBook.PublishDate.Should().Be(updatedBook.PublishDate);
        }

        [Fact]
        public void Fact_WhenValidDateTimeIsGiven_Book_ShouldBeUpdated()
        {

            UpdateBookCommand command = new(_context, _mapper);
            command.BookId = 1;
            command.Model = new UpdateBookModel() { PublishDate = DateTime.Now.Date.AddYears(-2) };


            FluentActions
                .Invoking(() => command.Handle()).Invoke();


            var updatedBook = _context.Books.FirstOrDefault(x => x.Id == command.BookId);
            updatedBook.Should().NotBeNull();
            updatedBook.Title.Should().Be(updatedBook.Title);
            updatedBook.AuthorId.Should().Be(updatedBook.AuthorId);
            updatedBook.GenreId.Should().Be(updatedBook.GenreId);
            updatedBook.PageCount.Should().Be(updatedBook.PageCount);
            updatedBook.PublishDate.Should().Be(command.Model.PublishDate);
        }
    }
}