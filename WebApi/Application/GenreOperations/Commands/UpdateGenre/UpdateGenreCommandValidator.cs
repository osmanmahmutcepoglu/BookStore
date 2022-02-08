using FluentValidation;

namespace WebApi.Application.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommandValidator : AbstractValidator<UpdateGenreCommand>
    {
        public UpdateGenreCommandValidator()
        {
            RuleFor(x => x.GenreId).GreaterThan(0);
            RuleFor(x => x.Model.Name).NotEmpty().MinimumLength(4);
        }
    }
}