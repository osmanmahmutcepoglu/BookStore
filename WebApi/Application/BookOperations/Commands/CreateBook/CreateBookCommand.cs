using AutoMapper;
using System;
using System.Linq;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.BookOperations.Commands.CreateBook
{
    public class CreateBookCommand
    {
        public CreateBookModel Model { get; set; } 
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;
        public CreateBookCommand(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Handle()
        {
            var book = _context.Books.FirstOrDefault(x => x.Title == Model.Title && x.AuthorId == Model.AuthorId && x.GenreId == Model.GenreId);
            if (book is not null)
            {
                throw new InvalidOperationException("The book is already in the system.");
            }


            book = _mapper.Map<Book>(Model); 

            _context.Books.Add(book);
            _context.SaveChanges();
        }
    }

    public class CreateBookModel
    { 
        public int AuthorId { get; set; }
        public int GenreId { get; set; }
        public string Title { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }
    }
}
