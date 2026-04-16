using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PropManageX.Models.Entities
{
    public class InvoiceModel
    {
        [Key]
        public int InvoiceID { get; set; }

        [ForeignKey("ContractModel")]
        public int ContractID { get; set; }

        public ContractModel Contract { get; set; }

        public string? Period { get; set; }
        public decimal Amount { get; set; }
        public DateTime DueDate { get; set; }
        public string? Status { get; set; }

    }
}
