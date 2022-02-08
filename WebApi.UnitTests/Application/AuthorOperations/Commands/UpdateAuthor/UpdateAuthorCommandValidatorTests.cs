using FluentAssertions;
using System;
using WebApi.Application.AuthorOperations.Commands.UpdateAuthor;
using Xunit;

namespace WebApi.UnitTests.Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandValidatorTests
    {
        [Fact]
        public void Fact_WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors()
        {
            
            UpdateAuthorCommand command = new(null, null);
            command.AuthorId = 0;
            command.Model = new UpdateAuthorModel() { FirstName = "", LastName = "", Birthdate = DateTime.Now};

            
            UpdateAuthorCommandValidator validator = new();
            var validationResult = validator.Validate(command);

            
            validationResult.Errors.Count.Should().BeGreaterThan(0);
        }

        [Theory] 
        [InlineData(0, "", "", "2021-11-26")]
        [InlineData(0, "A", "", "2021-11-26")]
        [InlineData(0, " ", "B", "2021-11-26")]
        public void Theory_WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(int authorId, string firstname, string lastname, string birthdate)
        {
            
            UpdateAuthorCommand command = new(null, null);
            command.AuthorId = authorId;
            command.Model = new UpdateAuthorModel() { FirstName = firstname, LastName = lastname, Birthdate = DateTime.Parse(birthdate)};

            
            UpdateAuthorCommandValidator validator = new();
            var validationResult = validator.Validate(command);

           
            validationResult.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void Fact_WhenDateTimeDateEqualsNowIsGiven_Validator_ShouldBeReturnError()
        {
            
            UpdateAuthorCommand command = new(null, null);
            command.AuthorId = 1;
            command.Model = new UpdateAuthorModel() { FirstName = "İrem", LastName = "Çalışkan", Birthdate = DateTime.Now};

            
            UpdateAuthorCommandValidator validator = new();
            var validationResult = validator.Validate(command);

           
            validationResult.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact] 
        public void WhenValidInputsAreGiven_Validator_ShouldBeReturnSuccess()
        {
            
            UpdateAuthorCommand command = new(null, null);
            command.AuthorId = 1;
            command.Model = new UpdateAuthorModel() { FirstName = "İrem", LastName = "Çalışkan", Birthdate = DateTime.Now.AddYears(-16) };

         
            UpdateAuthorCommandValidator validator = new();
            var validationResult = validator.Validate(command);

           
            validationResult.Errors.Count.Should().Be(0);
        }
    }
}
