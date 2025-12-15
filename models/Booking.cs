using System.ComponentModel.DataAnnotations;

namespace pz9_API.models
{
    public enum BookingStatus
    {
        Pending,
        Confirmed,
        Active,
        Completed,
        Cancelled
    }

    public class Booking
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public int TotalDays => (EndDate - StartDate).Days;

        [Range(1, 1000000)]
        public decimal TotalPrice { get; set; }

        public BookingStatus Status { get; set; } = BookingStatus.Pending;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public int CarId { get; set; }
        public int UserId { get; set; }

        public virtual Car Car { get; set; }
        public virtual User User { get; set; }
        public virtual Review Review { get; set; }
    }
}
