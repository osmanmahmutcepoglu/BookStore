using FluentAssertions;
using System;
using WebApi.Application.BookOperations.Commands.CreateBook;
using Xunit;

namespace WebApi.UnitTests.Application.BookOperations.Commands.CreateBook
{
    public class CreateBookCommandValidatorTests
    {
        [Fact]
        public void Fact_WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors()
        {
            
            CreateBookCommand command = new(null, null);
            command.Model = new CreateBookModel() { Title = "", PageCount = 0, PublishDate = DateTime.Now, GenreId = 0, AuthorId = 0 };

            
            CreateBookCommandValidator validator = new();
            var validationResult = validator.Validate(command);

          

           
            validationResult.Errors.Count.Should().BeGreaterThan(0);
        }

        [Theory]
        [InlineData("", 0, 0, 0)]
        [InlineData("Boo", 0, 0, 0)]
        [InlineData("Book", 0, 0, 0)]
        [InlineData("Book", 0, 0, 1)]
        [InlineData("Book", 0, 1, 0)]
        [InlineData("Book", 0, 1, 1)]
        [InlineData("Book", 100, 0, 0)]
        [InlineData("Book", 100, 1, 0)]
        [InlineData("Book", 100, 0, 1)]
        
        public void Theory_WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string title, int pageCount, int genreId, int authorId)
        {
            CreateBookCommand command = new(null, null);
            command.Model = new CreateBookModel() { Title = title, PageCount = pageCount, PublishDate = DateTime.Now.AddYears(-1), GenreId = genreId, AuthorId = authorId };

            
            CreateBookCommandValidator validator = new();
            var validationResult = validator.Validate(command);


            validationResult.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact] 
        public void Fact_WhenDateTimeDateEqualsNowIsGiven_Validator_ShouldBeReturnError()
        {
            
            CreateBookCommand command = new(null, null);
            command.Model = new CreateBookModel() { Title = "Book", PageCount = 100, PublishDate = DateTime.Now.Date, GenreId = 1, AuthorId = 1 };

           
            CreateBookCommandValidator validator = new();
            var validationResult = validator.Validate(command);


            validationResult.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact] 
        public void Fact_WhenValidInputsAreGiven_Validator_ShouldBeReturnSuccess()
        {
            
            CreateBookCommand command = new(null, null);
            command.Model = new CreateBookModel() { Title = "Book", PageCount = 100, PublishDate = DateTime.Now.Date.AddYears(-2), GenreId = 1, AuthorId = 1 };

            
            CreateBookCommandValidator validator = new();
            var validationResult = validator.Validate(command);


            validationResult.Errors.Count.Should().Be(0);
        }
    }
}