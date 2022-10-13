namespace ResourceControlingAPI.Dtos
{
    public class MeterReadingUpdateDto
    {
        public int ReadingNumbers { get; set; }

        public DateTime DateTimeReading { get; set; }

        public int MeterId { get; set; }
    }
}
