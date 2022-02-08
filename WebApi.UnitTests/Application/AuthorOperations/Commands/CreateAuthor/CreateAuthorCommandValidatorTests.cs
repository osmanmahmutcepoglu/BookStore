using FluentAssertions;
using System;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;
using Xunit;

namespace WebApi.UnitTests.Application.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommandValidatorTests
    {
        [Fact]
        public void Fact_WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors()
        {
            
            CreateAuthorCommand command = new(null, null);
            command.Model = new CreateAuthorModel() { FirstName = "", LastName = "", Birthdate = DateTime.Now };

        
            CreateAuthorCommandValidator validator = new();
            var validationResult = validator.Validate(command);

         
            validationResult.Errors.Count.Should().BeGreaterThan(0);
        }

        [Theory]
        [InlineData("A", "B", "2021-11-25")]
        [InlineData("", "", "2021-11-25")]
        [InlineData(null, null, "2021-11-25")]
        public void Theory_WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string firstName, string lastName, string birthdate)
        {
         
            CreateAuthorCommand command = new(null, null);
            command.Model = new CreateAuthorModel() { FirstName = firstName, LastName = lastName, Birthdate = DateTime.Parse(birthdate) };

            
            CreateAuthorCommandValidator validator = new();
            var validationResult = validator.Validate(command);

            
            validationResult.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void Fact_WhenDateTimeDateEqualsNowIsGiven_Validator_ShouldBeReturnError()
        {
           
            CreateAuthorCommand command = new(null, null);
            command.Model = new CreateAuthorModel() { FirstName = "Name", LastName = "Surname", Birthdate = DateTime.Now.Date };

           
            CreateAuthorCommandValidator validator = new();
            var validationResult = validator.Validate(command);

            validationResult.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact] 
        public void Fact_WhenValidInputsAreGiven_Validator_ShouldBeReturnSuccess()
        {
            
            CreateAuthorCommand command = new(null, null);
            command.Model = new CreateAuthorModel() { FirstName = "Name", LastName = "Surname", Birthdate = DateTime.Now.AddYears(-5) };

            
            CreateAuthorCommandValidator validator = new();
            var validationResult = validator.Validate(command);

            
            validationResult.Errors.Count.Should().Be(0);
        }
    }
}