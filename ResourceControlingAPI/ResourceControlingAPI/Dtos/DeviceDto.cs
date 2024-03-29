﻿using ResourceControlingAPI.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ResourceControlingAPI.Dtos
{
    public class DeviceDto
    {
        public int DeviceId { get; set; }
        public int AddressId { get; set; }
        [JsonIgnore]
        public Address? Address { get; set; }

        public int MeterId { get; set; }

        public double HoursForWaiting { get; set; }

        public string ErrorText { get; set; } = string.Empty;

        [JsonIgnore]
        public Meter? Meter { get; set; }
    }
}
