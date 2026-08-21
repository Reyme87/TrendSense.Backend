using FluentValidation;

namespace TrendSense.Application.Features.Stocks.Queries.GetStock
{
    public class GetStockQueryValidator : AbstractValidator<GetStockQuery>
    {
        public GetStockQueryValidator() 
        {
            RuleFor(x => x.SecId)
                .NotEmpty()
                .MaximumLength(12)
                .MinimumLength(1);
        }
    }
}
