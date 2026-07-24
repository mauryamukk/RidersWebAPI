using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RidersWebAPI.Models
{
    public class DriverLocation
    {
        [Key]
        public int Id { get; set; }

        public int DriverId { get; set; }

        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }

        public DateTime RecordedAt { get; set; }

        [ForeignKey(nameof(DriverId))]
        public Driver Driver { get; set; } = null!;
    }
}
