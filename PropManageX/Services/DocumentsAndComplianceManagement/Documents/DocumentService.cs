using PropManageX.DTOs.DTOsDocumentAndNotification.DocumentDTOs;
using PropManageX.Models.Context;
using PropManageX.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace PropManageX.Services.DocumentsAndComplianceManagement.Documents
{
    public class DocumentService : IDocumentService
    {
        private readonly PropManageXContext _context;
        private readonly IWebHostEnvironment _env;

        public DocumentService(PropManageXContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // Upload Document
        public async Task<DocumentDto> UploadDocument(CreateDocumentDto dto)
        {
            if (dto.File == null || dto.File.Length == 0)
                throw new Exception("File is empty");

            // Create folder if not exists
            var rootPath = _env.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            var folderPath = Path.Combine(rootPath, "documents");

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            // Generate unique file name
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(dto.File.FileName);

            var filePath = Path.Combine(folderPath, fileName);

            // Save file
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await dto.File.CopyToAsync(stream);
            }

            // Save to DB
            var document = new DocumentModel
            {
                EntityType = dto.EntityType,
                EntityID = dto.EntityID,
                DocumentType = dto.DocumentType,
                URI = fileName, // store only file name
                UploadedDate = DateTime.Now
            };

            _context.Documents.Add(document);
            await _context.SaveChangesAsync();

            return new DocumentDto
            {
                DocumentID = document.DocumentID,
                EntityType = document.EntityType,
                EntityID = document.EntityID,
                DocumentType = document.DocumentType,
                URI = document.URI,
                UploadedDate = document.UploadedDate
            };
        }

        // Get All Documents
        public async Task<List<DocumentDto>> GetAllDocuments()
        {
            return await _context.Documents
            .Select(d => new DocumentDto
            {
                DocumentID = d.DocumentID,
                EntityType = d.EntityType,
                EntityID = d.EntityID,
                DocumentType = d.DocumentType,
                URI = d.URI,
                UploadedDate = d.UploadedDate
            }).ToListAsync();
        }

        // Get Documents by Entity (IMPORTANT)
        public async Task<List<DocumentDto>> GetDocumentsByEntity(string entityType, int entityId)
        {
            return await _context.Documents
            .Where(d => d.EntityType == entityType && d.EntityID == entityId)
            .Select(d => new DocumentDto
            {
                DocumentID = d.DocumentID,
                EntityType = d.EntityType,
                EntityID = d.EntityID,
                DocumentType = d.DocumentType,
                URI = d.URI,
                UploadedDate = d.UploadedDate
            }).ToListAsync();
        }

        // Get Document By ID
        public async Task<DocumentDto> GetDocumentById(int id)
        {
            var doc = await _context.Documents.FindAsync(id);

            if (doc == null)
                return null;

            return new DocumentDto
            {
                DocumentID = doc.DocumentID,
                EntityType = doc.EntityType,
                EntityID = doc.EntityID,
                DocumentType = doc.DocumentType,
                URI = doc.URI,
                UploadedDate = doc.UploadedDate
            };
        }


        public async Task<bool> DeleteDocument(int id)
        {
            var doc = await _context.Documents.FindAsync(id);

            if (doc == null)
                return false;

            var filePath = Path.Combine(_env.WebRootPath, "documents", doc.URI);

            // Delete file from folder
            if (File.Exists(filePath))
                File.Delete(filePath);

            // Delete from DB
            _context.Documents.Remove(doc);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
