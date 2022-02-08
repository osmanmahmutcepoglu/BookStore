using FluentAssertions;
using WebApi.Application.GenreOperations.Commands.UpdateGenre;
using Xunit;

namespace WebApi.UnitTests.Application.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommandValidatorTests
    {
        [Fact]
        public void Fact_WhenInvalidInputIsGiven_Validator_ShouldBeReturnErrors()
        {
           
            UpdateGenreCommand command = new(null, null); 
            command.GenreId = 0; 
            command.Model = new UpdateGenreModel() { Name = "Gen" }; 

            UpdateGenreCommandValidator validator = new();
            var validationResult = validator.Validate(command);

           
            validationResult.Errors.Count.Should().BeGreaterThan(0);
        }

        [Theory] 
        [InlineData(0, null)]
        [InlineData(0, "")]
        [InlineData(0, "N")]
        [InlineData(0, "Na")]
        [InlineData(0, "Nam")]
        [InlineData(0, "Name")]
        [InlineData(1, null)]
        [InlineData(1, "")]
        [InlineData(1, "N")]
        [InlineData(1, "Na")]
        [InlineData(1, "Nam")]
       
        public void Theory_WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(int genreId, string name)
        {
            
            UpdateGenreCommand command = new(null, null); 
            command.GenreId = genreId;
            command.Model = new UpdateGenreModel() { Name = name };

           
            UpdateGenreCommandValidator validator = new();
            var validationResult = validator.Validate(command);

            
            validationResult.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact] 
        public void Fact_WhenValidInputIsGiven_Validator_ShouldBeReturnSuccess()
        {
            
            UpdateGenreCommand command = new(null, null);
            command.GenreId = 1;
            command.Model = new UpdateGenreModel() { Name = "Updated Genre" };

           
            UpdateGenreCommandValidator validator = new();
            var validationResult = validator.Validate(command);

           
            validationResult.Errors.Count.Should().Be(0);
        }

        [Theory] 
        [InlineData(1, "Genre 1")]
        [InlineData(16, "Genre 2")]
        [InlineData(66, "Genre 3")]
        [InlineData(100, "Genre 4")]
        public void Theory_WhenValidInputsAreGiven_Validator_ShouldBeReturnSuccess(int genreId, string name)
        {
            
            UpdateGenreCommand command = new(null, null); 
            command.GenreId = genreId;
            command.Model = new UpdateGenreModel() {Name = name};

           
            UpdateGenreCommandValidator validator = new();
            var validationResult = validator.Validate(command);

            
            validationResult.Errors.Count.Should().Be(0);
        }
    }
}