using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ResourceControlingAPI.Models
{
    public class MeterReading
    {
        [Key]
        public int MeterReadingId { get; set; }
        [Required]
        public int ReadingNumbers { get; set; }
        [Required]
        public DateTime? DateTimeReading { get; set; }

        [ForeignKey("MeterId")]
        public int MeterId { get; set; }

        [JsonIgnore]
        public Meter Meter { get; set; }

        
    }
}
