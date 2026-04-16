using PropManageX.DTOs.DTOsDocumentAndNotification.DocumentDTOs;

namespace PropManageX.Services.DocumentsAndComplianceManagement.Documents
{
    public interface IDocumentService
    {
        Task<DocumentDto> UploadDocument(CreateDocumentDto dto);

        Task<List<DocumentDto>> GetAllDocuments();

        Task<List<DocumentDto>> GetDocumentsByEntity(string entityType, int entityId);

        Task<DocumentDto> GetDocumentById(int id);

        Task<bool> DeleteDocument(int id);
    }
}
