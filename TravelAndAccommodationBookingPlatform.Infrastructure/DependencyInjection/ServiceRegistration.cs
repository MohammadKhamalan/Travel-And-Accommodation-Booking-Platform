using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TravelAndAccommodationBookingPlatform.Application.Interfaces;
using TravelAndAccommodationBookingPlatform.Core.Interfaces;
using TravelAndAccommodationBookingPlatform.Infrastructure.Data;
using TravelAndAccommodationBookingPlatform.Infrastructure.Persistence.Repository;
using TravelAndAccommodationBookingPlatform.Infrastructure.Services;


namespace TravelAndAccommodationBookingPlatform.Infrastructure.DependencyInjection
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    x => x.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)
                ));

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IHotelRepository, HotelRepository>();
            services.AddScoped<IRoomRepository, RoomRepository>();
            services.AddScoped<IBookingRepository, BookingRepository>();
            services.AddScoped<ICityRepository, CityRepository>();
            services.AddScoped<IReviewRepository, ReviewRepository>();
            services.AddScoped<IRoomTypeRepository, RoomTypeRepository>();
            services.AddScoped<IDiscountRepository, DiscountRepository>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<IRoomAmenityRepository, RoomAmenityRepository>();
            services.AddScoped<IOwnerRepository, OwnerRepository>();
            services.AddScoped<IImageRepository, ImageRepository>();


            services.AddScoped<ITokenService, JwtTokenService>();
            services.AddScoped<IInvoiceService, InvoiceService>();
            services.AddScoped<IPdfGenerator, PdfGeneratorService>();
            services.AddScoped<IEmailSender, EmailSender>();

            return services;
        }
    }
}
