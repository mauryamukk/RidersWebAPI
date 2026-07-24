using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RidersWebAPI.Models
{
    public class Driver
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }

        public bool IsOnline { get; set; }

        public bool IsAvailable { get; set; }

        public decimal? CurrentLatitude { get; set; }

        public decimal? CurrentLongitude { get; set; }

        public decimal? Rating { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        [ForeignKey(nameof(UserId))]
        public Users User { get; set; } = null!;

        public ICollection<Vehicle>? Vehicles { get; set; }

        public ICollection<DriverLocation>? DriverLocations { get; set; }

    }
}
