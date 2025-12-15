using System.ComponentModel.DataAnnotations;

namespace pz9_API.models
{
    public class Review
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Range(1, 5)]
        public int CarRating { get; set; } // Оценка автомобиля

        [Required]
        [Range(1, 5)]
        public int OwnerRating { get; set; } // Оценка владельца

        [StringLength(1000)]
        public string Comment { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Внешние ключи
        public int BookingId { get; set; } // Связь с бронированием
        public int CarId { get; set; }
        public int ReviewerId { get; set; } // Кто оставил отзыв (арендатор)
        public int OwnerId { get; set; } // О ком отзыв (владелец авто)

        // Навигационные свойства
        public virtual Booking Booking { get; set; }
        public virtual Car Car { get; set; }
        public virtual User Reviewer { get; set; }
        public virtual User Owner { get; set; }
    }
}
