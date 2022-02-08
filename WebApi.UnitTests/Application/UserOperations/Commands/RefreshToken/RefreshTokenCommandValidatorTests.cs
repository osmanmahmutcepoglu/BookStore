using FluentAssertions;
using WebApi.Application.UserOperations.Commands.RefreshToken;
using Xunit;

namespace WebApi.UnitTests.Application.UserOperations.Commands.RefreshToken
{
    public class RefreshTokenCommandValidatorTests
    {
        [Fact]
        public void Fact_WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors()
        {
            
            RefreshTokenCommand command = new(null, null);
            command.RefreshToken = "";

            
            RefreshTokenCommandValidator validator = new();
            var validationResult = validator.Validate(command);

            
            validationResult.Errors.Count.Should().BeGreaterThan(0);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        
        public void Theory_WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string refreshToken)
        {
            
            RefreshTokenCommand command = new(null, null);
            command.RefreshToken = refreshToken;

            
            RefreshTokenCommandValidator validator = new();
            var validationResult = validator.Validate(command);

         
            validationResult.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact] 
        public void Fact_WhenValidInputsAreGiven_Validator_ShouldBeReturnSuccess()
        {
            
            RefreshTokenCommand command = new(null, null);
            command.RefreshToken = "refreshToken";

            
            RefreshTokenCommandValidator validator = new();
            var validationResult = validator.Validate(command);

            
            validationResult.Errors.Count.Should().Be(0);
        }
    }
}