using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PropManageX.Models.Entities
{
    public class AmenityModel
    {
        [Key]
        public int AmenityID { get; set; }

        [ForeignKey("PropertyModel")]
        public int PropertyID { get; set; }
        public PropertyModel PropertyModel { get; set; }

        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}
