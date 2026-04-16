using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PropManageX.Models.Entities
{
    public class DealModel
    {
        [Key]
        public int DealID { get; set; }

        [ForeignKey("LeadModel")]
        public int LeadID { get; set; }
        public LeadModel LeadModel { get; set; }


        [ForeignKey("UnitModel")]
        public int UnitID { get; set; }
        public UnitModel UnitModel { get; set; }

        public string DealType { get; set; } //sale , Lease
        public decimal AgreedValue { get; set; }
        public DateTime ExpectedClosureDate { get; set; }
        public string Status { get; set; } //open , Booked , Cancelled

        public ICollection<ContractModel> Contracts { get; set; }
    }
}
