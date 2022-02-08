﻿using FluentAssertions;
using WebApi.Application.GenreOperations.Queries.GetGenreDetail;
using Xunit;

namespace WebApi.UnitTests.Application.GenreOperations.Queries.GetGenreDetail
{
    public class GetGenreDetailQueryValidatorTests
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(null)]
        public void Theory_WhenInvalidIdsAreGiven_Validator_ShouldBeReturnErrors(int genreId)
        {
           
            GetGenreDetailQuery query = new(null, null);
            query.GenreId = genreId;

           
            GetGenreDetailQueryValidator validator = new();
            var validationResult = validator.Validate(query);

            
            validationResult.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void Fact_WhenInvalidIdIsGiven_Validator_ShouldBeReturnErrors()
        {
            
            GetGenreDetailQuery query = new(null, null);
            query.GenreId = 0;

            
            GetGenreDetailQueryValidator validator = new();
            var validationResult = validator.Validate(query);

            
            validationResult.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void Fact_WhenInvalidIdIsGiven_Validator_ShouldBeReturnSuccess()
        {
            
            GetGenreDetailQuery query = new(null, null);
            query.GenreId = 1;

           
            GetGenreDetailQueryValidator validator = new();
            var validationResult = validator.Validate(query);

            
            validationResult.Errors.Count.Should().Be(0);
        }
    }
}