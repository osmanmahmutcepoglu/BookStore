using System.Linq;
using AutoMapper;
using FluentAssertions;
using WebApi.Application.AuthorOperations.Queries.GetAuthors;
using WebApi.DbOperations;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.AuthorOperations.Queries.GetAuthors
{
    public class GetAuthorsQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetAuthorsQueryTests(CommonTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }

        [Fact] 
        public void Fact_WhenModelListCountEqualsEntityListCount_ShouldBeReturnSuccess()
        {
            
            GetAuthorsQuery query = new(_context, _mapper);

            
            var queryList = FluentActions.Invoking(() => query.Handle()).Invoke().Count;

            
            var entityList = _context.Authors.ToList().Count;

            Assert.Equal(queryList, entityList);
        }
    }
}