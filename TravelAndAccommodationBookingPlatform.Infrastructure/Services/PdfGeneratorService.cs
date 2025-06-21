using NReco.PdfGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAndAccommodationBookingPlatform.Application.Interfaces;

namespace TravelAndAccommodationBookingPlatform.Infrastructure.Services
{
    public class PdfGeneratorService : IPdfGenerator
    {
        public async Task<byte[]> GeneratePdfFromHtml(string htmlContent)
        {
            var converter = new HtmlToPdfConverter();
            return await Task.FromResult(converter.GeneratePdf(htmlContent));
        }
    }
}
