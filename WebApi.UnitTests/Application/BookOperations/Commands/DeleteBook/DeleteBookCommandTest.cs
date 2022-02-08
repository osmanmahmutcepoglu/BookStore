using FluentAssertions;
using System;
using System.Linq;
using WebApi.Application.BookOperations.Commands.DeleteBook;
using WebApi.DbOperations;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommandTest : IClassFixture<CommonTestFixture> 
    {
        private readonly BookStoreDbContext _context;

        public DeleteBookCommandTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Theory]
     
        [InlineData(7)]
        [InlineData(8)]
        [InlineData(9)]
        [InlineData(10)]
        public void Theory_WhenNotExistedIdIsGiven_InvalidOperationException_ShouldBeReturn(int bookId)
        {

            DeleteBookCommand command = new(_context);
            command.BookId = bookId;

            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("No book found to be deleted!");
        }

        [Fact]
        public void Fact_WhenValidIdIsGiven_Book_ShouldBeDeleted()
        {

            DeleteBookCommand command = new(_context);
            command.BookId = 1;

            FluentActions.Invoking(() => command.Handle()).Invoke();

            var deletedBook = _context.Books.FirstOrDefault(x => x.Id == command.BookId);
            deletedBook.Should().BeNull();
        }
    }
}