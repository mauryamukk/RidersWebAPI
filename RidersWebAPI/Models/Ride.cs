using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RidersWebAPI.Models
{
    public class Ride
    {
        [Key]
        public int Id { get; set; }
        public int DriverId { get; set; }

        public int RiderId { get; set; }

        public byte Status { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public decimal? Distance { get; set; }

        public decimal? EstimatedFare { get; set; }

        public decimal? FinalFare { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        [ForeignKey(nameof(RiderId))]
        public RideRequest RideRequest { get; set; } = null!;

        [ForeignKey(nameof(DriverId))]
        public Driver Driver { get; set; } = null!;

        [ForeignKey(nameof(RiderId))]
        public Rider Rider { get; set; } = null!;
    }
}
