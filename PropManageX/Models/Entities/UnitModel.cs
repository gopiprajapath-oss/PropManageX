using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PropManageX.Models.Entities
{
    public class UnitModel
    {
        [Key]
        public int UnitID { get; set; }

        [ForeignKey("PropertyModel")]
        public int PropertyID { get; set; }
        public PropertyModel PropertyModel { get; set; }

        public int UnitNumber { get; set; }
        public double AreaSqFt { get; set; }
        public int BedroomCount { get; set; }
        public decimal BasePrice { get; set; }
        public string Status { get; set; }


        //Here we have to add more relationships
        public ICollection<DealModel> Deals { get; set; }
        public ICollection<MaintenanceRequestModel> MaintenanceRequests { get; set; }

    }
}
