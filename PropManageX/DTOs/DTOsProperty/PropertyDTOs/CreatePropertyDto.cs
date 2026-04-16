using System.ComponentModel.DataAnnotations;

namespace PropManageX.DTOs.PropertyDTOs.Property
{
    public class CreatePropertyDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Type { get; set; } //Residential/Commercial

        [Required]
        public string Location { get; set; }

        [Required]
        public int TotalUnits { get; set; }

        [Required]
        public string Status { get; set; } //Active/Inactive

    }
}
