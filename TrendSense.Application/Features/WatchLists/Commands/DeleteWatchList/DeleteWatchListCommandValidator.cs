using FluentValidation;

namespace TrendSense.Application.Features.WatchLists.Commands.DeleteWatchList
{
    public class DeleteWatchListCommandValidator : AbstractValidator<DeleteWatchListCommand>
    {
        public DeleteWatchListCommandValidator() 
        {
            RuleFor(x => x.WatchListId)
                .NotEqual(Guid.Empty)
                .WithMessage("WatchList ID is required.");
        }
    }
}
