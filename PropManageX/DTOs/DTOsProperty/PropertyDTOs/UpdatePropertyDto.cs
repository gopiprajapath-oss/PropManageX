using System.ComponentModel.DataAnnotations;

namespace PropManageX.DTOs.PropertyDTOs.Property
{
    public class UpdatePropertyDto
    {
        public string Name { get; set; }

        public string Type { get; set; }

        public string Location { get; set; }

        public int TotalUnits { get; set; }

        public string Status { get; set; }
    }
}
