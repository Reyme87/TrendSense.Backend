using FluentValidation;

namespace TrendSense.Application.Features.WatchLists.Commands.RemoveStockFromWatchList
{
    public class RemoveStockFromWatchListCommandValidator : AbstractValidator<RemoveStockFromWatchListCommand>
    {
        public RemoveStockFromWatchListCommandValidator() 
        {
            RuleFor(x => x.StockId)
                .NotEqual(Guid.Empty)
                .WithMessage("Stock ID is required.");
            
            RuleFor(x => x.WatchListId)
                .NotEqual(Guid.Empty)
                .WithMessage("WatchList ID is required.");
        }
    }
}
