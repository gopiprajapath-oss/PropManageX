using System.ComponentModel.DataAnnotations;

namespace PropManageX.DTOs.PropertyDTOs.AmenityDTOs
{
    public class UpdateAmenityDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
