using FluentAssertions;
using WebApi.Application.BookOperations.Queries.GetBookDetail;
using Xunit;

namespace WebApi.UnitTests.Application.BookOperations.Queries.GetBookDetail
{
    public class GetBookDetailQueryValidatorTests
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(null)]
        public void Theory_WhenInvalidIdIsGiven_Validator_ShouldBeReturnErrors(int bookId)
        {
            GetBookDetailQuery query = new(null, null);
            query.BookId = bookId;

            
            GetBookDetailQueryValidator validator = new();
            var validationResult = validator.Validate(query);

           
            validationResult.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void Fact_WhenInvalidIdIsGiven_Validator_ShouldBeReturnErrors()
        {
           
            GetBookDetailQuery query = new(null, null);
            query.BookId = 0;

            
            GetBookDetailQueryValidator validator = new();
            var validationResult = validator.Validate(query);

            
            validationResult.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void Fact_WhenInvalidIdIsGiven_Validator_ShouldBeReturnSuccess()
        {
           
            GetBookDetailQuery query = new(null, null);
            query.BookId = 1;

           
            GetBookDetailQueryValidator validator = new();
            var validationResult = validator.Validate(query);

            validationResult.Errors.Count.Should().Be(0);
        }
    }
}