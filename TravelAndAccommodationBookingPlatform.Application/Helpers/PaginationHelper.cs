using AutoMapper;
using TravelAndAccommodationBookingPlatform.Core.Models;

namespace TravelAndAccommodationBookingPlatform.Application.Helpers;

public static class PaginationHelper
{
    public static PaginatedList<TDestination> MapPaginatedList<TSource, TDestination>(
        PaginatedList<TSource> source,
        IMapper mapper)
    {
        var mappedItems = mapper.Map<List<TDestination>>(source.Items);
        return new PaginatedList<TDestination>(
            mappedItems,
            source.PageData.TotalItemCount,
            source.PageData.PageSize,
            source.PageData.CurrentPage
        );
    }
}
