
using System.ComponentModel.DataAnnotations;

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

        [Range(0,double.PositiveInfinity)]
        public double MaximumAvailableValue { get; set; }

    }
}
