using FluentAssertions;
using WebApi.Application.BookOperations.Commands.DeleteBook;
using Xunit;

namespace WebApi.UnitTests.Application.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommandValidatorTests
    {
        [Fact]
        public void Fact_WhenInvalidInputIsGiven_Validator_ShouldBeReturnErrors()
        {
            
            DeleteBookCommand command = new(null); 
            command.BookId = 0;

            DeleteBookCommandValidator validator = new();
            var validationResult = validator.Validate(command);


            validationResult.Errors.Count.Should().BeGreaterThan(0);
        }

        [Theory] 
        [InlineData(0)]
        [InlineData(-1)]
        public void Theory_WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(int bookId)
        {
            
            DeleteBookCommand command = new(null); 
            command.BookId = bookId;

            
            DeleteBookCommandValidator validator = new();
            var validationResult = validator.Validate(command);

            
            validationResult.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact] 
        public void Fact_WhenValidInputIsGiven_Validator_ShouldBeReturnSuccess()
        {
           
            DeleteBookCommand command = new(null);
            command.BookId = 1;

            
            DeleteBookCommandValidator validator = new();
            var validationResult = validator.Validate(command);

            
            validationResult.Errors.Count.Should().Be(0);
        }

        [Theory] 
        [InlineData(1)]
        [InlineData(16)]
        [InlineData(66)]
        [InlineData(100)]
        public void Theory_WhenValidInputsAreGiven_Validator_ShouldBeReturnSuccess(int bookId)
        {
            
            DeleteBookCommand command = new(null);
            command.BookId = bookId;

            
            DeleteBookCommandValidator validator = new();
            var validationResult = validator.Validate(command);

            validationResult.Errors.Count.Should().Be(0);
        }
    }
}