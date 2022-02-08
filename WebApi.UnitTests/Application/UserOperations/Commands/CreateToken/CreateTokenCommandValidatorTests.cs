﻿using FluentAssertions;
using WebApi.Application.UserOperations.Commands.CreateToken;
using Xunit;

namespace WebApi.UnitTests.Application.UserOperations.Commands.CreateToken
{
    public class CreateTokenCommandValidatorTests
    {
        [Fact]
        public void Fact_WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors()
        {
            
            CreateTokenCommand command = new(null, null);
            command.Model = new CreateTokenModel() { Email = "", Password = "" };

          
            CreateTokenCommandValidator validator = new();
            var validationResult = validator.Validate(command);

          
            validationResult.Errors.Count.Should().BeGreaterThan(0);
        }

        [Theory]
        [InlineData(null, null)]
        [InlineData("", "")]
        [InlineData(" ", " ")]
        [InlineData("abc", " ")]
        [InlineData(" ", "abc")]
        
        public void Theory_WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string email, string password)
        {
           
            CreateTokenCommand command = new(null, null);
            command.Model = new CreateTokenModel() { Email = email, Password = password };

            
            CreateTokenCommandValidator validator = new();
            var validationResult = validator.Validate(command);

            
            validationResult.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact] 
        public void Fact_WhenValidInputsAreGiven_Validator_ShouldBeReturnSuccess()
        {
            
            CreateTokenCommand command = new(null, null);
            command.Model = new CreateTokenModel() { Email = "email", Password = "password" };

            CreateTokenCommandValidator validator = new();
            var validationResult = validator.Validate(command);

            
            validationResult.Errors.Count.Should().Be(0);
        }
    }
}