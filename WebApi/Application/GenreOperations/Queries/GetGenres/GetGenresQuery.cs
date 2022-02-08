using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using WebApi.DbOperations;

namespace WebApi.Application.GenreOperations.Queries.GetGenres
{
    public class GetGenresQuery
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetGenresQuery(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<GenresViewModel> Handle()
        {
            var genreList = _context.Genres.OrderBy(x => x.Id).ToList();

            var vm = _mapper.Map<List<GenresViewModel>>(genreList);
            return vm;
        }
    }

    public class GenresViewModel
    { 
        public string Name { get; set; }
    }
}
