using Domain.Interfaces;
using Domain.Utilities;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Models.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.DomainRepositories
{
    public class DriveRepository : IDriveRepository
    {
        private readonly ApplicationDbContext context;
        private static readonly string[] Scopes = { DriveService.Scope.DriveFile };
        private static readonly string ApplicationName = "Talos";
        private static readonly string ServiceAccountEmail = "test-161@infinitevrtx.iam.gserviceaccount.com";
        private static readonly string ServiceAccountKeyFilePath = "C:\\infinitevrtx-639c1faca76a.json";
        private IProductRepository productRepository;
        private ICategoryRepository categoryRepository;
        private ICampaignRepository campaignRepository;

        public DriveRepository(ApplicationDbContext context, IProductRepository ProductRepository, ICategoryRepository CategoryRepository, ICampaignRepository campaignRepository)
        {
            this.context = context;
            this.productRepository = ProductRepository;
            this.categoryRepository = CategoryRepository;
            this.campaignRepository = campaignRepository;
        }

        public async Task<string> DeleteFile(string fileId)
        {
            var service = GetService();

            var request = service.Files.Delete(fileId);
            await request.ExecuteAsync();

            return $"El archivo con ID '{fileId}' se ha eliminado del Drive";
        }

        public string GetFileById(string fileId)
        {
            var service = GetService();

            var file = service.Files.Get(fileId).Execute();

            if (file == null)
            {
                return null;
            }

            using (var memoryStream = new MemoryStream())
            {
                service.Files.Get(fileId).Download(memoryStream);
                var base64String = Convert.ToBase64String(memoryStream.ToArray());
                return base64String;
            }
        }

        public IEnumerable<string> GetFiles()
        {
            var service = GetService();
            var fileList = service.Files.List();
            var result = fileList.Execute();

            var base64Files = new List<string>();

            foreach (var file in result.Files)
            {
                using (var memoryStream = new MemoryStream())
                {
                    service.Files.Get(file.Id).Download(memoryStream);
                    var base64String = Convert.ToBase64String(memoryStream.ToArray());
                    base64Files.Add(file.Id);
                }
            }

            return base64Files;
        }

        public List<KeyValuePair<string, string>> GetFilesByIds(List<string> fileIds)
        {
            var service = GetService();
            var base64Files = new List<KeyValuePair<string, string>>();

            foreach (var fileId in fileIds)
            {
                var file = service.Files.Get(fileId).Execute();

                if (file != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        service.Files.Get(fileId).Download(memoryStream);
                        var base64String = Convert.ToBase64String(memoryStream.ToArray());
                        base64Files.Add(new KeyValuePair<string, string>(fileId, base64String));
                    }
                }
            }

            return base64Files;
        }

        public async Task<string> UploadFile(List<IFormFile> files, string id)
        {
            foreach (var file in files)
            {
                try
                {
                    var service = GetService();

                    var fileMetadata = new Google.Apis.Drive.v3.Data.File()
                    {
                        Name = file.FileName
                    };

                    using (var memoryStream = new MemoryStream())
                    {
                        await file.CopyToAsync(memoryStream);

                        var uploadStream = new MemoryStream(memoryStream.ToArray());

                        var request = service.Files.Create(fileMetadata, uploadStream, file.ContentType);
                        request.Fields = "id";
                        request.Upload();

                        var fileUploaded = request.ResponseBody;
                        var imageBase64 = GetFileById(request.ResponseBody.Id);

                        productRepository.CreateImage(request.ResponseBody.Id, id,imageBase64);
                    }
                }
                catch
                {
                }
            }

            return "Archivos creados en Google Drive con exito";
        }

        public async Task<string> UploadCategoryFile(IFormFile file, string id)
        {
            try
            {
                var service = GetService();

                var fileMetadata = new Google.Apis.Drive.v3.Data.File()
                {
                    Name = file.FileName
                };

                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);

                    var uploadStream = new MemoryStream(memoryStream.ToArray());

                    var request = service.Files.Create(fileMetadata, uploadStream, file.ContentType);
                    request.Fields = "id";
                    request.Upload();

                    var fileUploaded = request.ResponseBody;
                    var imageBase64 = GetFileById(request.ResponseBody.Id);

                    var result = categoryRepository.CreateImage(request.ResponseBody.Id, id, imageBase64);

                    if (!result)
                        await DeleteFile(request.ResponseBody.Id);
                }
            }
            catch
            {
                return "Error al subir el archivo";
            }

            return "Archivo creados en Google Drive con exito";
        }

        public async Task<string> UploadCampaignFile(IFormFile file, string id)
        {
            try
            {
                var service = GetService();

                var fileMetadata = new Google.Apis.Drive.v3.Data.File()
                {
                    Name = file.FileName
                };

                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);

                    var uploadStream = new MemoryStream(memoryStream.ToArray());

                    var request = service.Files.Create(fileMetadata, uploadStream, file.ContentType);
                    request.Fields = "id";
                    request.Upload();

                    var fileUploaded = request.ResponseBody;
                    var imageBase64 = GetFileById(request.ResponseBody.Id);

                    var result = campaignRepository.CreateImage(request.ResponseBody.Id, id, imageBase64);

                    if (!result)
                        await DeleteFile(request.ResponseBody.Id);
                }
            }
            catch
            {
                return "Error al subir el archivo";
            }

            return "Archivo creados en Google Drive con exito";
        }

        private DriveService GetService()
        {
            var stream = new FileStream(ServiceAccountKeyFilePath, FileMode.Open, FileAccess.Read);
            var test = stream.Length;
            var credential = GoogleCredential.FromStream(stream);
            if (credential.IsCreateScopedRequired)
            {
                credential = credential.CreateScoped(new string[] { DriveService.Scope.Drive });
            }

            var service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName
            });

            return service;
        }
    }
}
