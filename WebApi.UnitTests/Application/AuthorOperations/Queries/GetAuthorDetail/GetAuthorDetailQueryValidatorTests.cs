using FluentAssertions;
using WebApi.Application.AuthorOperations.Queries.GetAuthorDetail;
using Xunit;

namespace WebApi.UnitTests.Application.AuthorOperations.Queries.GetAuthorDetail
{
    public class GetAuthorDetailQueryValidatorTests
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(null)]
        public void Theory_WhenInvalidIdIsGiven_Validator_ShouldBeReturnErrors(int authorId)
        {
        
            GetAuthorDetailQuery query = new(null, null);
            query.AuthorId = authorId;

         
            GetAuthorDetailQueryValidator validator = new();
            var validationResult = validator.Validate(query);

          
            validationResult.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void Fact_WhenInvalidIdIsGiven_Validator_ShouldBeReturnErrors()
        {
            
            GetAuthorDetailQuery query = new(null, null);
            query.AuthorId = 0;

           
            GetAuthorDetailQueryValidator validator = new();
            var validationResult = validator.Validate(query);

            
            validationResult.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void Fact_WhenInvalidIdIsGiven_Validator_ShouldBeReturnSuccess()
        {
            GetAuthorDetailQuery query = new(null, null);
            query.AuthorId = 1;

            
            GetAuthorDetailQueryValidator validator = new();
            var validationResult = validator.Validate(query);

            
            validationResult.Errors.Count.Should().Be(0);
        }
    }
}