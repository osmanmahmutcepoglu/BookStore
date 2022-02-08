using System;
using FluentAssertions;
using WebApi.Application.BookOperations.Commands.UpdateBook;
using Xunit;

namespace WebApi.UnitTests.Application.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommandValidatorTests
    {
        [Fact]
        public void Fact_WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors()
        {

            UpdateBookCommand command = new(null, null);
            command.BookId = 0;
            command.Model = new UpdateBookModel() { Title = "", PageCount = 0, PublishDate = DateTime.Now, GenreId = 0, AuthorId = 0 };

            UpdateBookCommandValidator validator = new();
            var validationResult = validator.Validate(command);


            validationResult.Errors.Count.Should().BeGreaterThan(0);
        }

        [Theory] 
        [InlineData(0, "", 0, 0, 0)]
        [InlineData(0, "Boo", 0, 0, 0)]
        [InlineData(0, "Book", 0, 0, 0)]
        [InlineData(0, "Book", 0, 0, 1)]
        [InlineData(0, "Book", 0, 1, 0)]
        [InlineData(0, "Book", 0, 1, 1)]
        [InlineData(0, "Book", 100, 0, 0)]
        [InlineData(0, "Book", 100, 1, 0)]
        [InlineData(0, "Book", 100, 0, 1)]
        [InlineData(1, "", 0, 0, 0)]
        [InlineData(1, "Boo", 0, 0, 0)]
        [InlineData(1, "Book", 0, 0, 0)]
        [InlineData(1, "Book", 0, 0, 1)]
        [InlineData(1, "Book", 0, 1, 0)]
        [InlineData(1, "Book", 0, 1, 1)]
        [InlineData(1, "Book", 100, 0, 0)]
        [InlineData(1, "Book", 100, 1, 0)]
        [InlineData(1, "Book", 100, 0, 1)]

        public void Theory_WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(int bookId, string title, int pageCount, int genreId, int authorId)
        {

            UpdateBookCommand command = new(null, null); 
            command.BookId = bookId;
            command.Model = new UpdateBookModel() { Title = title, PageCount = pageCount, PublishDate = DateTime.Now.AddYears(-1), GenreId = genreId, AuthorId = authorId };

            UpdateBookCommandValidator validator = new();
            var validationResult = validator.Validate(command);

            validationResult.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void Fact_WhenDateTimeDateEqualsNowIsGiven_Validator_ShouldBeReturnError()
        {

            UpdateBookCommand command = new(null, null);
            command.BookId = 1;
            command.Model = new UpdateBookModel() { Title = "Book", PageCount = 100, PublishDate = DateTime.Now.Date, GenreId = 1, AuthorId = 1 };


            UpdateBookCommandValidator validator = new();
            var validationResult = validator.Validate(command);


            validationResult.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact] 
        public void WhenValidInputsAreGiven_Validator_ShouldBeReturnSuccess()
        {
            UpdateBookCommand command = new(null, null);
            command.BookId = 1;
            command.Model = new UpdateBookModel() { Title = "Book", PageCount = 100, PublishDate = DateTime.Now.Date.AddYears(-2), GenreId = 1, AuthorId = 1 };

            UpdateBookCommandValidator validator = new();
            var validationResult = validator.Validate(command);

            validationResult.Errors.Count.Should().Be(0);
        }
    }
}