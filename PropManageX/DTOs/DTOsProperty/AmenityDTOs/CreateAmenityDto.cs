using System.ComponentModel.DataAnnotations;

namespace PropManageX.DTOs.PropertyDTOs.AmenityDTOs
{
    public class CreateAmenityDto
    {
        [Required]
        public int PropertyID { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
