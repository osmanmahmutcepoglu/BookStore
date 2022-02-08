using System;
using AutoMapper;
using FluentAssertions;
using WebApi.Application.AuthorOperations.Queries.GetAuthorDetail;
using WebApi.DbOperations;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.AuthorOperations.Queries.GetAuthorDetail
{
    public class GetAuthorDetailQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetAuthorDetailQueryTests(CommonTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public void Fact_WhenNotExistAuthorIdIsGiven_InvalidOperationException_ShouldBeReturn()
        {
          
            GetAuthorDetailQuery query = new(_context, _mapper);
            query.AuthorId = 0;

            
            FluentActions
                .Invoking(() => query.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Author is not found!");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(11)]
        [InlineData(999)]
        public void Theory_WhenNotExistAuthorIdsAreGiven_InvalidOperationException_ShouldBeReturn(int authorId)
        {
            
            GetAuthorDetailQuery query = new(_context, _mapper);
            query.AuthorId = authorId;

            
            FluentActions
                .Invoking(() => query.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Author is not found!");
        }

        [Fact] 
        public void Fact_WhenExistAuthorIdIsGiven_BookDetails_ShouldBeReturn()
        {
            
            GetAuthorDetailQuery query = new(_context, _mapper);
            query.AuthorId = 1;

            
            var authorDetail = FluentActions.Invoking(() => query.Handle()).Invoke();
            authorDetail.Should().NotBeNull();
        }
    }
}