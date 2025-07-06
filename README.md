# Travel and Accommodation Booking Platform 

> Welcome to the **Travel and Accommodation Booking Platform**, a state-of-the-art solution designed to streamline and enhance the management of travel bookings, accommodations, and related services. Our platform offers a robust API capable of handling a diverse range of operations, including user authentication, booking management, city and hotel administration, discounts, reviews, and more.

---

## Tech Stack

| Layer         | Technology                               |
|---------------|-------------------------------------------|
| Backend       | ASP.NET Core Web API (.NET 8)            |
| Architecture  | Clean Architecture + CQRS + MediatR      |
| Database      | SQL Server 2022                          |
| ORM           | Entity Framework Core                    |
| Auth          | JWT Bearer Tokens                        |
| Payment       | Stripe Integration                       |
| Validation    | FluentValidation                         |
| Testing       | xUnit + Moq (`TravelAndAccommodationBookingPlatform.Tests`) |
| Docs          | Swagger / OpenAPI                        |
| CI/CD         | GitHub Actions Pipelines ready           |

---

## Project Structure

```
📦 TravelAndAccommodationBookingPlatform
├── 📁 TravelAndAccommodationBookingPlatform.API           # Presentation Layer (Web API)
├── 📁 TravelAndAccommodationBookingPlatform.Application   # Application Layer (CQRS, DTOs, Handlers)
├── 📁 TravelAndAccommodationBookingPlatform.Core          # Domain Layer (Entities, Enums, Interfaces)
├── 📁 TravelAndAccommodationBookingPlatform.Infrastructure# Infrastructure Layer (EF Core, Repositories, Services)
├── 📁 TravelAndAccommodationBookingPlatform.Tests         # Unit Test Project (xUnit + Moq)
├── 📁 .github/workflows                                   # GitHub Actions CI/CD pipeline
└── 📄 README.md                                            # Project overview
```

---

## Local Development Setup

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download)
- SQL Server 2022
- Visual Studio 2022+ or VS Code

---

## Default Configuration

Your `appsettings.json` (already included):

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=TravelAndAccommodationBookingPlatform;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

---

## Authentication

The API uses **JWT Bearer Tokens** for secure access control. You can test it in Swagger:

- Use `/api/Authentication/login` with valid user credentials.
- Paste the returned token into the `Authorize` button in Swagger to unlock protected endpoints.

### Authorization Attributes Guide

| Attribute                    | Description                                                                 |
|-----------------------------|-----------------------------------------------------------------------------|
| `[Authorize]`               | Requires the user to be authenticated (JWT token must be provided).         |
| `[Authorize(Roles = "Admin")]` | Requires the user to be authenticated and have the Admin role.               |
| `[AllowAnonymous]`          | Allows **public access** (no login required), even if `[Authorize]` is applied globally.|

> The platform defaults to `[Authorize]` globally or at controller level. Use `[AllowAnonymous]` to expose public endpoints.

---

## Stripe Payments

Set your Stripe keys in `appsettings.json`:

```json
"Stripe": {
  "PublishableKey": "your_publishable_key",
  "SecretKey": "your_secret_key"
}
```

---

## Run Migrations

```bash
cd TravelAndAccommodationBookingPlatform.WebApi

dotnet ef migrations add AddAmenityRoomTypeRelation --project Infrastructure --startup-project WebApi
dotnet ef database update --project Infrastructure --startup-project WebApi
```

---

## Endpoint Access Legend

| Symbol     | Description                      |
|------------|----------------------------------|
| ✅         | Requires authentication           |
| ✅ Admin   | Requires Admin role              |
| ❌         | Public access (`[AllowAnonymous]`) |

---

## Endpoints

### Authentication

| Endpoint                     | Method | Description                         | Auth |
|-----------------------------|--------|-------------------------------------|------|
| `/api/Authentication/login` | POST   | User login, returns JWT token       | ❌   |

**Body**:
```json
{
  "email": "mohammad.khamalan@gmail.com",
  "password": "P@ssw0rd"
}
```

---

### Users

| Endpoint                   | Method | Description             | Auth |
|---------------------------|--------|-------------------------|------|
| `/api/users`              | GET    | Get all users           | ✅ Admin  |
| `/api/users/{id}`         | GET    | Get user by ID          | ✅ Admin |
| `/api/users/register`     | POST   | Register a new user     | ❌   |
| `/api/users/{id}`         | DELETE | Delete a user           | ✅ Admin  |

---

### Hotels

| Endpoint                                | Method | Description                                             | Auth      |
|----------------------------------------|--------|---------------------------------------------------------|-----------|
| `/api/hotels`                          | GET    | Get all hotels                                          | ❌        |
| `/api/hotels/{id}`                     | GET    | Get hotel by ID                                         | ❌        |
| `/api/hotels`                          | POST   | Create new hotel                                        | ✅ Admin  |
| `/api/hotels/{id}`                     | PUT    | Update hotel                                            | ✅ Admin  |
| `/api/hotels/{id}`                     | DELETE | Delete hotel                                            | ✅ Admin  |
| `/api/hotels/featured`                 | GET    | Get Featured Deal hotel                                 | ❌        |
| `/api/hotels/{hotelId}/available-rooms`| GET    | Get available rooms for a specified hotel               | ❌        |
| `/api/hotels/search`                   | POST   | Search hotels based on filter parameter                 | ❌        |

---

### Rooms

| Endpoint                              | Method | Description                             | Auth      |
|--------------------------------------|--------|-----------------------------------------|-----------|
| `/api/rooms`                         | GET    | Get all rooms                           | ❌        |
| `/api/rooms/{id}`                    | GET    | Get room by ID                          | ❌        |
| `/api/rooms`                         | POST   | Create new room                         | ✅ Admin  |
| `/api/rooms/{id}`                    | PUT    | Update room                             | ✅ Admin  |
| `/api/rooms/{id}`                    | DELETE | Delete room                             | ✅ Admin  |
| `/api/rooms/hotel/{hotelId}`         | GET    | Get rooms by hotel ID                   | ❌        |
| `/api/rooms/{roomId}/final-price`    | GET    | Get room final price after discounts    | ❌        |

---

### Bookings

| Endpoint                                  | Method | Description                              | Auth |
|------------------------------------------|--------|------------------------------------------|------|
| `/api/bookings/{bookingId}`              | GET    | Get booking by ID                         | ✅   |
| `/api/bookings/pending`                  | POST   | Create pending booking                    | ✅   |
| `/api/bookings/{bookingId}`              | DELETE | Cancel a booking                          | ✅   |
| `/api/bookings/{bookingId}/invoice`      | GET    | Get booking invoice (PDF)                 | ✅   |
| `/api/bookings/can-book`                 | GET    | Check room availability                   | ✅   |
| `/api/bookings/check-existnece`          | GET    | Check booking existence for guest         | ✅   |
| `/api/bookings/by-hotel/{hotelId}`       | GET    | Paginated bookings for a hotel            | ✅   |
| `/api/bookings/pending/{pendingBookingId}`| DELETE | Delete a pending booking                  | ✅   |

---

### Payments

| Endpoint                          | Method | Description                             | Auth |
|----------------------------------|--------|-----------------------------------------|------|
| `/api/payments/confirm-booking` | POST   | Confirm pending booking with payment    | ✅   |

---

### Cities

| Endpoint                     | Method | Description                    | Auth       |
|-----------------------------|--------|--------------------------------|------------|
| `/api/cities`               | GET    | Get all cities                 | ✅         |
| `/api/cities/trending`      | GET    | Get trending cities            | ✅         |
| `/api/cities`               | POST   | Add a city                     | ✅ Admin   |
| `/api/cities/{id}`          | GET    | Get specific city              | ✅ Admin   |
| `/api/cities/{id}`          | PUT    | Update a specific city         | ✅ Admin   |
| `/api/cities/{id}`          | DELETE | Delete a city                  | ✅ Admin   |

---

### Reviews

| Endpoint                                         | Method | Description                                 | Auth |
|--------------------------------------------------|--------|---------------------------------------------|------|
| `/api/reviews/hotel/{hotelId}`                  | GET    | Get reviews by hotel                        | ❌   |
| `/api/reviews/booking/{bookingId}/exists`       | GET    | Check if booking has a review               | ✅   |
| `/api/reviews/{reviewId}`                       | GET    | Get review by ID                            | ❌   |
| `/api/reviews`                                  | POST   | Create a review                             | ✅   |
| `/api/reviews/{id}`                             | PUT    | Update a review                             | ✅   |
| `/api/reviews/{id}`                             | DELETE | Delete a review                             | ✅   |

---

### Discounts

| Endpoint                                       | Method | Description                                 | Auth       |
|-----------------------------------------------|--------|---------------------------------------------|------------|
| `/api/discounts/roomtype/{roomTypeId}`        | GET    | Get discounts for a room type               | ✅         |
| `/api/discounts`                              | POST   | Create a discount                           | ✅ Admin   |
| `/api/discounts/{id}`                         | GET    | Get discount by ID                          | ✅         |
| `/api/discounts/{id}`                         | DELETE | Delete discount                             | ✅         |
| `/api/discounts/overlap`                      | GET    | Check for overlapping discounts             | ✅         |

---

### Images

| Endpoint                        | Method | Description                        | Auth      |
|--------------------------------|--------|------------------------------------|-----------|
| `/api/images`                  | POST   | Upload new image                   | ✅ Admin  |
| `/api/images/hotel/{hotelId}`  | GET    | Get images by hotel ID             | ❌        |
| `/api/images/city/{cityId}`    | GET    | Get images by city ID              | ❌        |
| `/api/images/{id}`             | GET    | Get image details                  | ❌        |
| `/api/images/{id}`             | DELETE | Delete image                       | ✅ Admin  |

---

### Owners

| Endpoint              | Method | Description                        | Auth      |
|----------------------|--------|------------------------------------|-----------|
| `/api/owners`        | GET    | Get all owners                     | ✅ Admin  |
| `/api/owners`        | POST   | Create a new owner                 | ✅ Admin  |
| `/api/owners/{id}`   | GET    | Get owner by ID                    | ✅ Admin  |
| `/api/owners/{id}`   | PUT    | Update owner info                  | ✅ Admin  |
| `/api/owners/{id}`   | DELETE | Delete owner                       | ✅ Admin  |

---

### Room Amenities

| Method | Endpoint               | Description                           | Auth      |
|--------|------------------------|---------------------------------------|-----------|
| GET    | `/api/amenities`       | Get all room amenities (paginated)    | ✅        |
| GET    | `/api/amenities/{id}`  | Get specific amenity by ID            | ✅        |
| POST   | `/api/amenities`       | Create new amenity                    | ✅ Admin  |
| PUT    | `/api/amenities/{id}`  | Update amenity                        | ✅ Admin  |
| DELETE | `/api/amenities/{id}`  | Delete amenity                        | ✅ Admin  |

---

### Room Types

| Method | Endpoint                      | Description                                  | Auth      |
|--------|-------------------------------|----------------------------------------------|-----------|
| GET    | `/api/room-types/{hotelId}`   | Get room types by hotel                      | ✅ Admin  |
| POST   | `/api/room-types`             | Create new room type                         | ✅ Admin  |

---

## Best Practices Followed

- Clean Architecture separation
- CQRS with MediatR
- Swagger API documentation
- Stripe integration for secure payments
- EF Core with SQL Server and Migrations

---


## Contact

- **Author**: Mohammad Khamalan  
- **Email**: mohammad.khamalan@gmail.com  
- **GitHub**: https://github.com/MohammadKhamalan

---

Thank you for your interest. I look forward to hearing from you!
