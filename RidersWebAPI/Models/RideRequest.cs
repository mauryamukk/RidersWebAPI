using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RidersWebAPI.Models
{
    public class RideRequest
    {
        [Key]
        public int Id { get; set; }

        public int RiderId { get; set; }

        public decimal PickupLatitude { get; set; }

        public decimal PickupLongitude { get; set; }

        public decimal DropLatitude { get; set; }

        public decimal DropLongitude { get; set; }

        public byte Status { get; set; }

        public int? MatchedDriverId { get; set; }

        public DateTime RequestedAt { get; set; }

        public DateTime? CancelledAt { get; set; }

        [ForeignKey(nameof(RiderId))]
        public Rider Rider { get; set; } = null!;

        [ForeignKey(nameof(MatchedDriverId))]
        public Driver? Driver { get; set; }
    }
}
