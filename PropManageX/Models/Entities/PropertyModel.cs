using System.ComponentModel.DataAnnotations;

namespace PropManageX.Models.Entities
{
    public class PropertyModel
    {
        [Key]
        public int PropertyID { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Location { get; set; }
        public int TotalUnits { get; set; }
        public string Status { get; set; }

        //Relationships
        public ICollection<UnitModel> Units { get; set; }
        public ICollection<AmenityModel> Amenities { get; set; }
        public ICollection<LeadModel> Leads { get; set; }
    }
}
