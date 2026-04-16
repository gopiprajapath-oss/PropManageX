using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PropManageX.Models.Entities
{
    public class SiteVisitModel
    {
        [Key]
        public int VisitID { get; set; }

        [ForeignKey("LeadModel")]
        public int LeadID { get; set; }
        public LeadModel Lead { get; set; } 

        public DateTime VisitDate { get; set; }

        [ForeignKey("UserModel")]
        public int AgentID { get; set; }
        public UserModel Agent { get; set; }


        public string Notes { get; set; }

        //Many SiteVists One Lead
        //Many SiteVists - One Agent
    }
}
