using Microsoft.AspNetCore.Http;
using Models;
using Models.Classes;
using Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IDriveRepository
    {
        IEnumerable<string> GetFiles();
        string GetFileById(string fileId);
        List<KeyValuePair<string, string>> GetFilesByIds(List<string> fileIds);
        Task<string> UploadFile(List<IFormFile> files, string id);
        Task<string> UploadCategoryFile(IFormFile files, string id);
        Task<string> DeleteFile(string fileId);
    }
}
