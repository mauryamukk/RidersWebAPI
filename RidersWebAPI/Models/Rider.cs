using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RidersWebAPI.Models
{
    public class Rider
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }

        public decimal? Rating { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        [ForeignKey(nameof(UserId))]
        public Users User { get; set; } = null!;

        public ICollection<RideRequest>? RideRequests { get; set; }

        public ICollection<Ride>? Rides { get; set; }
    }
}
