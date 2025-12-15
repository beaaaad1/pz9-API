using System.ComponentModel.DataAnnotations;

namespace pz9_API.models
{
    public enum CarStatus
    {
        Available,
        Rented,
        Maintenance,
        Unavailable
    }

    public enum FuelType
    {
        Petrol,
        Diesel,
        Electric,
        Hybrid
    }

    public class Car
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Brand { get; set; }

        [Required]
        [StringLength(100)]
        public string Model { get; set; }

        [Range(1990, 2024)]
        public int Year { get; set; }

        [StringLength(20)]
        public string Color { get; set; }

        [Required]
        [StringLength(20)]
        public string LicensePlate { get; set; }

        public FuelType FuelType { get; set; }

        [Range(1, 9)]
        public int Seats { get; set; } = 5;

        [Range(1, 100000)]
        public decimal PricePerDay { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public CarStatus Status { get; set; } = CarStatus.Available;

        [Required]
        public string Location { get; set; } 


        public int OwnerId { get; set; }


        public virtual User Owner { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
