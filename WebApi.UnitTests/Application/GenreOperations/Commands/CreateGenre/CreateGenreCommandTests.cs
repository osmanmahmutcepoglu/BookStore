using AutoMapper;
using FluentAssertions;
using System;
using System.Linq;
using WebApi.Application.GenreOperations.Commands.CreateGenre;
using WebApi.DbOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.GenreOperations.Commands.CreateGenre
{
    public class CreateGenreCommandTests : IClassFixture<CommonTestFixture> 
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateGenreCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact] 
        public void Fact_WhenAlreadyExistGenreNameIsGiven_InvalidOperationException_ShouldBeReturn() 
        {
           
            var genre = new Genre() { Name = "Genre"};
            _context.Genres.Add(genre);
            _context.SaveChanges();

            CreateGenreCommand command = new(_context, _mapper);
            command.Model = new CreateGenreModel() { Name = genre.Name};

            
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("The genre is already in the system.");
        }

        [Fact] 
        public void Fact_WhenValidInputsAreGiven_Genre_ShouldBeCreated()
        {
           
            CreateGenreCommand command = new(_context, _mapper);
            command.Model = new CreateGenreModel() { Name = "Genre"};

           
            FluentActions.Invoking(() => command.Handle()).Invoke();

            var addedGenre = _context.Genres.FirstOrDefault(x => x.Name == command.Model.Name);
            addedGenre.Should().NotBeNull();
            addedGenre.Name.Should().Be(command.Model.Name);
        }

        [Theory]

        [InlineData("")]
        [InlineData("n")]
        [InlineData("na")]
        [InlineData("nam")]
        [InlineData("name")] 
        [InlineData("name1")]
        public void Theory_WhenValidNamesAreGiven_Genre_ShouldBeCreated(string name)
        {
            
            CreateGenreCommand command = new(_context, _mapper);
            command.Model = new CreateGenreModel() { Name = name };

            
            FluentActions.Invoking(() => command.Handle()).Invoke();

            
            var addedGenre = _context.Genres.FirstOrDefault(x => x.Name == command.Model.Name);
            addedGenre.Should().NotBeNull();
            addedGenre.Name.Should().Be(command.Model.Name);
        }
    }
}