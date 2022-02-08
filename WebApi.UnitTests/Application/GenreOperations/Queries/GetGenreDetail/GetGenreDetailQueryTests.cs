using AutoMapper;
using FluentAssertions;
using System;
using WebApi.Application.GenreOperations.Queries.GetGenreDetail;
using WebApi.DbOperations;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.GenreOperations.Queries.GetGenreDetail
{
    public class GetGenreDetailQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetGenreDetailQueryTests(CommonTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public void Fact_WhenNotExistGenreIdIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            
            GetGenreDetailQuery query = new(_context, _mapper);
            query.GenreId = 0;

            
            FluentActions
                .Invoking(() => query.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Genre is not found!");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(11)]
        [InlineData(999)]
        public void Theory_WhenNotExistGenreIdsAreGiven_InvalidOperationException_ShouldBeReturn(int genreId)
        {
            
            GetGenreDetailQuery query = new(_context, _mapper);
            query.GenreId = genreId;

            
            FluentActions
                .Invoking(() => query.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Genre is not found!");
        }

        [Fact] 
        public void Fact_WhenExistGenreIdIsGiven_GenreDetails_ShouldBeReturn()
        {
            
            GetGenreDetailQuery query = new(_context, _mapper);
            query.GenreId = 1;

           
             var genreDetail = FluentActions.Invoking(() => query.Handle()).Invoke();
             genreDetail.Should().NotBeNull();
        }
    }
}