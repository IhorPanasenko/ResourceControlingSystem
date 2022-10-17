namespace ResourceControlingAPI.Dtos
{
    public class MeterReadingDtoUpdate
    {
        public int ReadingNumbers { get; set; }

        public DateTime? DateTimeReading { get; set; }

        public int MeterId { get; set; }
    }
}
