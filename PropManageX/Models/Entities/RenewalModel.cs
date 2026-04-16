using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PropManageX.Models.Entities
{
    public class RenewalModel
    {
        [Key]
        public int RenewalID { get; set; }

        [ForeignKey("ContractModel")]
        public int ContractID { get; set; }
        public ContractModel ContractModel { get; set; }

        public decimal ProposedValue { get; set; }
        public DateTime ProposedEndDate { get; set; }
        public string Status { get; set; }

        //Add Relationships

    }
}
