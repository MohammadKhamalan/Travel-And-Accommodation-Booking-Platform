using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAndAccommodationBookingPlatform.Core.Models;

public class PageData
{
    public int TotalItemCount { get; set; }
    public int PageSize { get; set; }
    public int CurrentPage { get; set; }
    public int TotalPageCount =>
        PageSize == 0 ? 0 : (int)Math.Ceiling(TotalItemCount / (double)PageSize);

    public PageData(int totalItemCount, int pageSize, int currentPage)
    {
        TotalItemCount = totalItemCount;
        PageSize = pageSize;
        CurrentPage = currentPage;
    }
}
