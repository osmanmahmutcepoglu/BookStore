using AutoMapper;
using System;
using System.Linq;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommand
    {
        public int BookId { get; set; }
        public UpdateBookModel Model { get; set; }

        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;
        public UpdateBookCommand(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Handle()
        {
            var book = _context.Books.FirstOrDefault(x => x.Id == BookId);
            if (book is null)
            {
                throw new InvalidOperationException("No book found to be updated!");
            }


            var vm = _mapper.Map<Book>(book);
            vm.AuthorId = Model.AuthorId != default ? Model.AuthorId : book.AuthorId;
            vm.GenreId = Model.GenreId != default ? Model.GenreId : book.GenreId; 
            vm.Title = Model.Title != default ? Model.Title : book.Title; 
            vm.PageCount = Model.PageCount != default ? Model.PageCount : book.PageCount; 
            vm.PublishDate = Model.PublishDate != default ? Model.PublishDate : book.PublishDate; 

            _context.SaveChanges();
        }
    }

    public class UpdateBookModel
    { 
        public int AuthorId { get; set; }
        public int GenreId { get; set; }
        public string Title { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }
    }
}