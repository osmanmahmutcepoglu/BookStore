using FluentAssertions;
using WebApi.Application.GenreOperations.Commands.DeleteGenre;
using Xunit;

namespace WebApi.UnitTests.Application.GenreOperations.Commands.DeleteGenre
{
    public class DeleteGenreCommandValidatorTests
    {
        [Fact]
        public void Fact_WhenInvalidInputIsGiven_Validator_ShouldBeReturnErrors()
        {
            
            DeleteGenreCommand command = new(null); 
            command.GenreId = 0;

            
            DeleteGenreCommandValidator validator = new();
            var validationResult = validator.Validate(command);

           
            validationResult.Errors.Count.Should().BeGreaterThan(0);
        }

        [Theory] 
        [InlineData(0)]
        [InlineData(-1)]
        public void Theory_WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(int genreId)
        {
            
            DeleteGenreCommand command = new(null); 
            command.GenreId = genreId;

            
            DeleteGenreCommandValidator validator = new();
            var validationResult = validator.Validate(command);

            
            validationResult.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact] 
        public void WhenValidInputIsGiven_Validator_ShouldBeReturnSuccess()
        {
           
            DeleteGenreCommand command = new(null);
            command.GenreId = 1;

           
            DeleteGenreCommandValidator validator = new();
            var validationResult = validator.Validate(command);

            
            validationResult.Errors.Count.Should().Be(0);
        }

        [Theory] 
        [InlineData(1)]
        [InlineData(16)]
        [InlineData(66)]
        [InlineData(100)]
        public void Theory_WhenValidInputsAreGiven_Validator_ShouldBeReturnSuccess(int genreId)
        {
            
            DeleteGenreCommand command = new(null); 
            command.GenreId = genreId;

            
            DeleteGenreCommandValidator validator = new();
            var validationResult = validator.Validate(command);

           
            validationResult.Errors.Count.Should().Be(0);
        }
    }
}