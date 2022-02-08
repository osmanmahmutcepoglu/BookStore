using AutoMapper;
using FluentAssertions;
using System;
using WebApi.Application.BookOperations.Queries.GetBookDetail;
using WebApi.DbOperations;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.BookOperations.Queries.GetBookDetail
{
    public class GetBookDetailQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetBookDetailQueryTests(CommonTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public void Fact_WhenNotExistBookIdIsGiven_InvalidOperationException_ShouldBeReturn()
        {
           
            GetBookDetailQuery query = new(_context, _mapper);
            query.BookId = 0;

           
            FluentActions
                .Invoking(() => query.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Book is not found!");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(11)]
        [InlineData(999)]
        public void Theory_WhenNotExistBookIdsAreGiven_InvalidOperationException_ShouldBeReturn(int bookId)
        {
            
            GetBookDetailQuery query = new(_context, _mapper);
            query.BookId = bookId;

            
            FluentActions
                .Invoking(() => query.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Book is not found!");
        }

        [Fact] 
        public void Fact_WhenExistBookIdIsGiven_BookDetails_ShouldBeReturn()
        {
            
            GetBookDetailQuery query = new(_context, _mapper);
            query.BookId = 1;

            
             var bookDetail = FluentActions.Invoking(() => query.Handle()).Invoke();
             bookDetail.Should().NotBeNull();
        }
    }
}