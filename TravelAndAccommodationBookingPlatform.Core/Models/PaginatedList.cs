using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAndAccommodationBookingPlatform.Core.Models;

public class PaginatedList<T>
{
    public List<T> Items { get; set; }
    public PageData PageData { get; set; }

    public PaginatedList(List<T> items, int totalItemCount, int pageSize, int currentPage)
    {
        Items = items;
        PageData = new PageData(totalItemCount, pageSize, currentPage);
    }
}
