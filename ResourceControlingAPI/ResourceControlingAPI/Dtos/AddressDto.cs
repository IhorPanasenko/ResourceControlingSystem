﻿using System.ComponentModel.DataAnnotations;

namespace ResourceControlingAPI.Dtos
{
    public class AddressDto
    {
        [Key]
        public int AddressId { get; set; }

        [Required]
        public string? City { get; set; }

        [Required]
        public string? StreetName { get; set; }

        [Required]
        public int HouseNumber { get; set; }

        [Required]
        public int FlatNumber { get; set; }
    }
}
