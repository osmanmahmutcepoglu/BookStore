using FluentAssertions;
using WebApi.Application.AuthorOperations.Commands.DeleteAuthor;
using Xunit;

namespace WebApi.UnitTests.Application.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommandValidatorTests
    {
        [Fact]
        public void Fact_WhenInvalidInputIsGiven_Validator_ShouldBeReturnErrors()
        {
            
            DeleteAuthorCommand command = new(null); 
            command.AuthorId = 0;

            
            DeleteAuthorCommandValidator validator = new();
            var validationResult = validator.Validate(command);

            
            validationResult.Errors.Count.Should().BeGreaterThan(0);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Theory_WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(int authorId)
        {
            
            DeleteAuthorCommand command = new(null); 
            command.AuthorId = authorId;

           
            DeleteAuthorCommandValidator validator = new();
            var validationResult = validator.Validate(command);

           
            validationResult.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact] 
        public void Fact_WhenValidInputIsGiven_Validator_ShouldBeReturnSuccess()
        {
            
            DeleteAuthorCommand command = new(null);
            command.AuthorId = 1;

           
            DeleteAuthorCommandValidator validator = new();
            var validationResult = validator.Validate(command);

            
            validationResult.Errors.Count.Should().Be(0);
        }

        [Theory] 
        [InlineData(1)]
        [InlineData(16)]
        [InlineData(66)]
        [InlineData(100)]
        public void Theory_WhenValidInputsAreGiven_Validator_ShouldBeReturnSuccess(int authorId)
        {
            
            DeleteAuthorCommand command = new(null); 
            command.AuthorId = authorId;

            
            DeleteAuthorCommandValidator validator = new();
            var validationResult = validator.Validate(command);

            
            validationResult.Errors.Count.Should().Be(0);
        }
    }
}