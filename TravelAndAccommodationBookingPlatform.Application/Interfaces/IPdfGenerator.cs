using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAndAccommodationBookingPlatform.Application.Interfaces
{
    public interface IPdfGenerator
    {
        public Task<byte[]> GeneratePdfFromHtml(string htmlContent);
    }
}
