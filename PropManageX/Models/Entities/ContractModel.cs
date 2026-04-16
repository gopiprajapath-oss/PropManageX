using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PropManageX.Models.Entities
{
    public class ContractModel
    {
        [Key]
        public int ContractID { get; set; }

        [ForeignKey("DealModel")]
        public int DealID { get; set; }
        public DealModel Deal { get; set; }

        public string? ContractType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal ContractValue { get; set; }
        public string? Status { get; set; }


        //add Relationships
        public ICollection<InvoiceModel> Invoices { get; set; }
    }
}
