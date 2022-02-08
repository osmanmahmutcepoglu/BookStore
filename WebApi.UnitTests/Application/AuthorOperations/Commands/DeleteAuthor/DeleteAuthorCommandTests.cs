﻿using FluentAssertions;
using System;
using System.Linq;
using WebApi.Application.AuthorOperations.Commands.DeleteAuthor;
using WebApi.DbOperations;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;

        public DeleteAuthorCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Theory]
        [InlineData(5)]
        [InlineData(6)]
        [InlineData(7)]
        [InlineData(8)]
        [InlineData(9)]
        [InlineData(10)]
        public void Theory_WhenNotExistedIdsAreGiven_InvalidOperationException_ShouldBeReturn(int authorId)
        {

            DeleteAuthorCommand command = new(_context);
            command.AuthorId = authorId;

            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("No author found to be deleted!");
        }

        [Fact]
        public void Fact_WhenValidIdIsGiven_Author_ShouldBeDeleted()
        {
            
            var authorId = 1;

            _context.Books.RemoveRange(_context.Books.Where(x => x.AuthorId == authorId));
            _context.SaveChanges();

            DeleteAuthorCommand command = new(_context);
            command.AuthorId = authorId;

           
            FluentActions.Invoking(() => command.Handle()).Invoke();

          
            var deletedBook = _context.Authors.FirstOrDefault(x => x.Id == command.AuthorId);
            deletedBook.Should().BeNull();
        }

        [Fact]
        public void Fact_WhenValidIdIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            
            DeleteAuthorCommand command = new(_context);
            command.AuthorId = 1;

            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Author has a published book in the system!");
        }
    }
}