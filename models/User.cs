using System.ComponentModel.DataAnnotations;

namespace pz9_API.models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(20)]
        public string Phone { get; set; }

        public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;

        public bool IsVerified { get; set; } = false;

        [Range(0, 5)]
        public double Rating { get; set; } = 0.0;
        
        public int CarsOwned { get; set; } = 0;
        public int BookingsMade { get; set; } = 0;

       
        public virtual ICollection<Car> OwnedCars { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; }
        public virtual ICollection<Review> ReviewsGiven { get; set; }
        public virtual ICollection<Review> ReviewsReceived { get; set; }
    }

}
