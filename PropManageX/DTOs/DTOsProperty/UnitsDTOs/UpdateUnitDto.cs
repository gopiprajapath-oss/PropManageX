using System.ComponentModel.DataAnnotations;

namespace PropManageX.DTOs.PropertyDTOs.UnitsDTOs
{
    public class UpdateUnitDto
    {
        [Required]
        public int UnitNumber { get; set; }

        public double AreaSqFt { get; set; }

        public int BedroomCount { get; set; }

        public decimal BasePrice { get; set; }

        public string Status { get; set; }
    }
}
