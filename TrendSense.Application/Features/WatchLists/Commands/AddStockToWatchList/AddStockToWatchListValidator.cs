using FluentValidation;

namespace TrendSense.Application.Features.WatchLists.Commands.AddStockToWatchList
{
    public class AddStockToWatchListValidator : AbstractValidator<AddStockToWatchListCommand>
    {
        public AddStockToWatchListValidator()
        {
            RuleFor(x => x.WatchListId)
                .NotEqual(Guid.Empty)
                .WithMessage("WatchList ID is required.");

            RuleFor(x => x.StockId)
                .NotEqual(Guid.Empty)
                .WithMessage("Stock ID is required.");
        }
    }
}
