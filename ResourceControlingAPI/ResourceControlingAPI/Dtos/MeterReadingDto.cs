using ResourceControlingAPI.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ResourceControlingAPI.Dtos
{
    public class MeterReadingDto
    {
        [Key]
        public int MeterReadingId { get; set; }

        [Required]
        public int ReadingNumbers { get; set; }

        [Required]
        public DateTime? DateTimeReading { get; set; }

        public int MeterId { get; set; }

        public MeterDto? Meter { get; set; }
    }
}
