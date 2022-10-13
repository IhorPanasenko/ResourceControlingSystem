using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResourceControlingAPI.Dtos
{
    public class MeterReadingDto
    {
        [Key]
        public int MeterReadingId { get; set; }

        [Required]
        public int ReadingNumbers { get; set; }

        [Required]
        public DateTime DateTimeReading { get; set; }

        public int MeterId { get; set; }

    }
}
