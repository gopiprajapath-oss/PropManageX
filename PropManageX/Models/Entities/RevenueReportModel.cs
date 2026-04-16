using System.ComponentModel.DataAnnotations;

namespace PropManageX.Models.Entities
{
    public class RevenueReportModel
    {
        [Key]
        public int ReportID { get; set; }

        public string Scope { get; set; }
        public string Metrics { get; set; }

        public DateTime GeneratedDate { get; set; }

        //Add relationshops

    }
}
