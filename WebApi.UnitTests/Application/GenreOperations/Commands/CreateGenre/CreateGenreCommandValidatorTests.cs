using FluentAssertions;
using WebApi.Application.GenreOperations.Commands.CreateGenre;
using Xunit;

namespace WebApi.UnitTests.Application.GenreOperations.Commands.CreateGenre
{
    public class CreateGenreCommandValidatorTests
    {
        [Fact]
        public void Fact_WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors()
        {
            
            CreateGenreCommand command = new(null, null); 
            command.Model = new CreateGenreModel() { Name = "" };

            
            CreateGenreCommandValidator validator = new();
            var validationResult = validator.Validate(command);

            
            validationResult.Errors.Count.Should().BeGreaterThan(0);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("a")]
        [InlineData("ab")]
        [InlineData("abc")]
        
        public void Theory_WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string name)
        {
            
            CreateGenreCommand command = new(null, null); 
            command.Model = new CreateGenreModel() { Name = name };

           
            CreateGenreCommandValidator validator = new();
            var validationResult = validator.Validate(command);

            
            validationResult.Errors.Count.Should().BeGreaterThan(0);
        }

        
        [Fact] 
        public void WhenValidInputsAreGiven_Validator_ShouldBeReturnSuccess()
        {
           
            CreateGenreCommand command = new(null, null); 
            command.Model = new CreateGenreModel() { Name = "Genre" };

            
            CreateGenreCommandValidator validator = new();
            var validationResult = validator.Validate(command);

            
            validationResult.Errors.Count.Should().Be(0);
        }
    }
}