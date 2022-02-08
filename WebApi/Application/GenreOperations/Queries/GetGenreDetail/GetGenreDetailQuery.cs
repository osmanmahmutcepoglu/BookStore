using AutoMapper;
using System;
using System.Linq;
using WebApi.DbOperations;

namespace WebApi.Application.GenreOperations.Queries.GetGenreDetail
{
    public class GetGenreDetailQuery
    {
        public int GenreId { get; set; }

        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetGenreDetailQuery(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public GenreDetailViewModel Handle()
        {
            var genre = _context.Genres.Where(x => x.Id == GenreId).FirstOrDefault();
            if (genre is null)
            {
                throw new InvalidOperationException("Genre is not found!");
            }

            var vm = _mapper.Map<GenreDetailViewModel>(genre);
            return vm;
        }
    }

    public class GenreDetailViewModel
    { 
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
