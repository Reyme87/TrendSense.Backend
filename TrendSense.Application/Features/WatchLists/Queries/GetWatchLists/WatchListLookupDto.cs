using AutoMapper;
using TrendSense.Application.Common.Mappings;
using TrendSense.Domain;

namespace TrendSense.Application.Features.WatchLists.Queries.GetWatchLists
{
    public class WatchListLookupDto : IMapWith<WatchList>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IList<WatchListItemDto> Items { get; set; } = [];

        public void Mapping(Profile profile)
        {
            profile.CreateMap<WatchList, WatchListLookupDto>()
                .ForMember(listDto => listDto.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(listDto => listDto.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(listDto => listDto.Items, opt => opt.MapFrom(src => src.Items));
        }
    }
}
