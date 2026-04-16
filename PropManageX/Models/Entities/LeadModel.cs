using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PropManageX.Models.Entities
{
    public class LeadModel
    {
        [Key]
        public int LeadID { get; set; }


        [ForeignKey("PropertyModel")]
        public int PropertyID { get; set; }
        public PropertyModel PropertyModel { get; set; }

        public string CustomerName { get; set; }
        public string ContactInfo { get; set; }
        public string InterestType { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Status { get; set; }


        //Here also we need some more relations

        public ICollection<DealModel> Deals { get; set; }   
        public ICollection<SiteVisitModel> SiteVisits { get; set; }
    }
}
