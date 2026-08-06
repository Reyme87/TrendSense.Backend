using AutoMapper;
using TrendSense.Application.Common.Mappings;
using TrendSense.Domain;

namespace TrendSense.Application.Features.WatchLists.Queries.GetWatchLists
{
    public class WatchListItemDto : IMapWith<WatchListItem>
    {
        public Guid StockId { get; set; }

        public string TickerSymbol { get; set; } = null!;

        public string Name { get; set; } = null!;

        public double? LastPrice { get; set; }

        public double? DayChange { get; set; }

        public double? DayChangePercent { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<WatchListItem, WatchListItemDto>()
                .ForMember(x => x.StockId,
                    opt => opt.MapFrom(s => s.StockId))
                .ForMember(x => x.TickerSymbol,
                    opt => opt.MapFrom(s => s.Stock.TickerSymbol))
                .ForMember(x => x.Name,
                    opt => opt.MapFrom(s => s.Stock.Name))
                .ForMember(x => x.LastPrice,
                    opt => opt.MapFrom(s => s.Stock.LastPrice))
                .ForMember(x => x.DayChange,
                    opt => opt.MapFrom(s => s.Stock.DayChange))
                .ForMember(x => x.DayChangePercent,
                    opt => opt.MapFrom(s => s.Stock.DayChangePercent));
        }
    }
}
