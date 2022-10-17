using System.ComponentModel.DataAnnotations;

namespace ResourceControlingAPI.Dtos
{
    public class MeterDtoUpdate
    {
        public int Number { get; set; }
        public string? Purpose { get; set; }

        [Range(0, double.PositiveInfinity)]
        public double MaximumAvailableValue { get; set; }

        public double ResourcePrice { get; set; }

        public int DeviceId { get; set; }
    }
}
