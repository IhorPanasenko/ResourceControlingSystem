
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ResourceControlingAPI.Models
{
    public class Meter
    {
        [Key]
        public int MeterId { get; set; }
        [Required]
        public int Number { get; set; }

        [Required]
        public string? Purpose { get; set; }

        [Range(1,double.PositiveInfinity)]
        public double MaximumAvailableValue { get; set; }

        public List<MeterReading> meterReadings { get; set; }

        public Device Device { get; set; }

    }
}
