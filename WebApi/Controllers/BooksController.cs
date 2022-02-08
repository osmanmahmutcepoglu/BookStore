using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.BookOperations.Commands.CreateBook;
using WebApi.Application.BookOperations.Commands.DeleteBook;
using WebApi.Application.BookOperations.Commands.UpdateBook;
using WebApi.Application.BookOperations.Queries.GetBookDetail;
using WebApi.Application.BookOperations.Queries.GetBooks;
using WebApi.DbOperations;

namespace WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")] 
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;


        public BooksController(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetBooks() 
        {
            GetBooksQuery query = new(_context, _mapper);
            var result = query.Handle();
            return Ok(result);
        }

        [HttpGet("{id}")] 
        public IActionResult GetById([FromRoute] int id)
        {
            BookDetailViewModel result;

            GetBookDetailQuery query = new(_context, _mapper);
            query.BookId = id;

            GetBookDetailQueryValidator validator = new();
            validator.ValidateAndThrow(query);

            result = query.Handle();

            return Ok(result);
        }

        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel newBook)
        {
 
            CreateBookCommand command = new(_context, _mapper);
            command.Model = newBook;
            CreateBookCommandValidator validator = new();
            validator.ValidateAndThrow(command); 
            command.Handle();

            return Ok();
        }

        [HttpPut("{id}")] 
        public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel updatedBook)
        {

            UpdateBookCommand command = new(_context, _mapper);
            command.BookId = id;
            command.Model = updatedBook;
            UpdateBookCommandValidator validator = new();
            validator.ValidateAndThrow(command);
            command.Handle();

            return Ok();
        }

        [HttpDelete("{id}")] 
        public IActionResult DeleteBook(int id)
        {

            DeleteBookCommand command = new(_context);
            command.BookId = id;
            DeleteBookCommandValidator validator = new();
            validator.ValidateAndThrow(command);
            command.Handle();

            return Ok();
        }
    }
}
