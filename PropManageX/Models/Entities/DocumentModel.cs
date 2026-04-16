using System.ComponentModel.DataAnnotations;

namespace PropManageX.Models.Entities
{
    public class DocumentModel
    {
        [Key]
        public int DocumentID { get; set; }

        public string EntityType { get; set; }
        public int EntityID { get; set; }

        public string DocumentType { get; set; }
        public string URI { get; set; }

        public DateTime UploadedDate { get; set; }

        //Add relationships

    }
}
