using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RidersWebAPI.Models
{
    public class Vehicle
    {
        [Key]
        public int Id { get; set; }

        public int DriverId { get; set; }

        public string VehicleNumber { get; set; } = string.Empty;

        public string VehicleType { get; set; } = string.Empty;

        public string Brand { get; set; } = string.Empty;

        public string Model { get; set; } = string.Empty;

        public string Color { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        [ForeignKey(nameof(DriverId))]
        public Driver Driver { get; set; } = null!;
    }
}
