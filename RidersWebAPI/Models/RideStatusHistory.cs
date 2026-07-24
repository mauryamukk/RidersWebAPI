using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RidersWebAPI.Models
{
    public class RideStatusHistory
    {
        [Key]
        public int Id { get; set; }

        public int RideId { get; set; }

        public byte Status { get; set; }

        public string? Remarks { get; set; }

        public DateTime CreatedAt { get; set; }

        [ForeignKey(nameof(RideId))]
        public Ride Ride { get; set; } = null!;
    }
}
