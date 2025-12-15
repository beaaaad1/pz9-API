using Microsoft.EntityFrameworkCore;
using pz9_API.models;

namespace pz9_API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Review> Reviews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Car>()
                .HasOne(c => c.Owner)
                .WithMany(u => u.OwnedCars)
                .HasForeignKey(c => c.OwnerId)
                .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Car)
                .WithMany(c => c.Bookings)
                .HasForeignKey(b => b.CarId);

            modelBuilder.Entity<Booking>()
                .HasOne(b => b.User)
                .WithMany(u => u.Bookings)
                .HasForeignKey(b => b.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Review>()
                .HasOne(r => r.Booking)
                .WithOne(b => b.Review)
                .HasForeignKey<Review>(r => r.BookingId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Review>()
                .HasOne(r => r.Reviewer)
                .WithMany(u => u.ReviewsGiven)
                .HasForeignKey(r => r.ReviewerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Review>()
                .HasOne(r => r.Owner)
                .WithMany(u => u.ReviewsReceived)
                .HasForeignKey(r => r.OwnerId)
                .OnDelete(DeleteBehavior.Restrict); 
        }

        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var users = new List<User>
            {
                new User { Id = 1, Username = "Owner1", Email = "owner1@mail.com", Phone = "111-222-3333", IsVerified = true, Rating = 4.5, CarsOwned = 2 },
                new User { Id = 2, Username = "Owner2", Email = "owner2@mail.com", Phone = "222-333-4444", IsVerified = true, Rating = 4.8, CarsOwned = 3 },
                new User { Id = 3, Username = "Renter1", Email = "renter1@mail.com", Phone = "333-444-5555", IsVerified = true, Rating = 4.2, BookingsMade = 3 },
                new User { Id = 4, Username = "Renter2", Email = "renter2@mail.com", Phone = "444-555-6666", IsVerified = false, Rating = 0.0 },
                new User { Id = 5, Username = "TestUser", Email = "test@mail.com", Phone = "555-666-7777", IsVerified = true, Rating = 4.0 }
            };
                modelBuilder.Entity<User>().HasData(users);

            var cars = new List<Car>
            {
                new Car { Id = 1, Brand = "Toyota", Model = "Camry", Year = 2020, LicensePlate = "A123BC", PricePerDay = 3500, FuelType = FuelType.Petrol, Seats = 5, Location = "Центр", OwnerId = 1, Status = CarStatus.Available },
                new Car { Id = 2, Brand = "Tesla", Model = "Model 3", Year = 2023, LicensePlate = "E456FG", PricePerDay = 6000, FuelType = FuelType.Electric, Seats = 5, Location = "Аэропорт", OwnerId = 1, Status = CarStatus.Available },
                new Car { Id = 3, Brand = "BMW", Model = "X5", Year = 2019, LicensePlate = "H789IJ", PricePerDay = 7500, FuelType = FuelType.Diesel, Seats = 5, Location = "Север", OwnerId = 2, Status = CarStatus.Available },
                new Car { Id = 4, Brand = "Kia", Model = "Rio", Year = 2022, LicensePlate = "K012LM", PricePerDay = 2500, FuelType = FuelType.Petrol, Seats = 5, Location = "Юг", OwnerId = 2, Status = CarStatus.Available },
                new Car { Id = 5, Brand = "Mercedes", Model = "C-Class", Year = 2021, LicensePlate = "N345OP", PricePerDay = 5000, FuelType = FuelType.Hybrid, Seats = 5, Location = "Восток", OwnerId = 2, Status = CarStatus.Available }
            };
            modelBuilder.Entity<Car>().HasData(cars);

            var bookings = new List<Booking>
            {
                new Booking { Id = 1, CarId = 1, UserId = 3, StartDate = DateTime.UtcNow.AddDays(1), EndDate = DateTime.UtcNow.AddDays(3), TotalPrice = 7000, Status = BookingStatus.Confirmed }, // Camry
                new Booking { Id = 2, CarId = 3, UserId = 3, StartDate = DateTime.UtcNow.AddDays(5), EndDate = DateTime.UtcNow.AddDays(8), TotalPrice = 22500, Status = BookingStatus.Pending }, // X5
                new Booking { Id = 3, CarId = 1, UserId = 4, StartDate = DateTime.UtcNow.AddDays(-5), EndDate = DateTime.UtcNow.AddDays(-3), TotalPrice = 7000, Status = BookingStatus.Completed }, // Camry (Completed)
                new Booking { Id = 4, CarId = 4, UserId = 5, StartDate = DateTime.UtcNow.AddDays(10), EndDate = DateTime.UtcNow.AddDays(11), TotalPrice = 2500, Status = BookingStatus.Confirmed }, // Rio
                new Booking { Id = 5, CarId = 2, UserId = 5, StartDate = DateTime.UtcNow.AddDays(15), EndDate = DateTime.UtcNow.AddDays(17), TotalPrice = 12000, Status = BookingStatus.Pending } // Tesla
            };
                    modelBuilder.Entity<Booking>().HasData(bookings);

            var reviews = new List<Review>
            {
                new Review { Id = 1, BookingId = 3, CarId = 1, ReviewerId = 4, OwnerId = 1, CarRating = 5, OwnerRating = 5, Comment = "Отличная машина, владелец пунктуальный." },
        
                new Review { Id = 2, BookingId = 1, CarId = 1, ReviewerId = 3, OwnerId = 1, CarRating = 4, OwnerRating = 4, Comment = "Все хорошо, но немного грязновата." },
                new Review { Id = 3, BookingId = 2, CarId = 3, ReviewerId = 3, OwnerId = 2, CarRating = 5, OwnerRating = 5, Comment = "Шикарный автомобиль, никаких проблем." },
                new Review { Id = 4, BookingId = 4, CarId = 4, ReviewerId = 5, OwnerId = 2, CarRating = 3, OwnerRating = 4, Comment = "Соотношение цена/качество ок." },
                new Review { Id = 5, BookingId = 5, CarId = 2, ReviewerId = 5, OwnerId = 1, CarRating = 5, OwnerRating = 5, Comment = "Тесла - огонь!" }
            };
                    modelBuilder.Entity<Review>().HasData(reviews);
        }
        
    }
}