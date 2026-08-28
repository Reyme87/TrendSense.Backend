using FluentValidation;

namespace TrendSense.Application.Features.Stocks.Queries.GetPriceHistory
{
    public class GetPriceHistoryQueryValidator : AbstractValidator<GetPriceHistoryQuery>
    {
        public GetPriceHistoryQueryValidator()
        {
            RuleFor(x => x.StockId)
                .NotEqual(Guid.Empty)
                .WithMessage("Stock ID is required.");
        }
    }
}
