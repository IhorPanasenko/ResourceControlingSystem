using System.ComponentModel.DataAnnotations;

namespace ResourceControlingAPI.Dtos
{
    public class MeterDto
    {
        [Key]
        public int MeterId { get; set; }
        [Required]
        public int Number { get; set; }

        [Required]
        public string? Purpose { get; set; }

        [Range(0, double.PositiveInfinity)]
        public double MaximumAvailableValue { get; set; }
    }
}
