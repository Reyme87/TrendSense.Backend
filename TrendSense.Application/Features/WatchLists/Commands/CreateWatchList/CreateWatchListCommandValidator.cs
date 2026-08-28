using FluentValidation;

namespace TrendSense.Application.Features.WatchLists.Commands.CreateWatchList
{
    public class CreateWatchListCommandValidator : AbstractValidator<CreateWatchListCommand>
    {
        public CreateWatchListCommandValidator() 
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(50);
        }
    }
}
