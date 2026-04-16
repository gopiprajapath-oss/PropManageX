using System.ComponentModel.DataAnnotations;

namespace PropManageX.DTOs.DTOsDocumentAndNotification.DocumentDTOs
{
    public class CreateDocumentDto
    {
        [Required]
        public string EntityType { get; set; } // Lead / Deal / Contract

        [Required]
        public int EntityID { get; set; }

        [Required]
        public string DocumentType { get; set; }

        [Required]
        public IFormFile File { get; set; }
    }
}
