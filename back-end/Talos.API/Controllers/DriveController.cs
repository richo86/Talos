using Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Talos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DriveController : ControllerBase
    {
        private IDriveRepository driveRepository;

        public DriveController(IDriveRepository DriveRepository)
        {
            this.driveRepository = DriveRepository;
        }

        [HttpGet("GetAllFiles")]
        public IEnumerable<string> GetFiles()
        {
            return driveRepository.GetFiles();
        }

        [HttpGet("GetFileById/{fileId}")]
        public IActionResult GetFileById(string fileId)
        {
            try
            {
                var file = driveRepository.GetFileById(fileId);

                if (file == null)
                    return NotFound();

                return Ok(file);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost("GetFilesByIds")]
        public IActionResult GetFilesByIds([FromBody] List<string> fileIds)
        {
            try
            {
                var files = driveRepository.GetFilesByIds(fileIds);

                if (files.Count == 0)
                    return NotFound();

                return Ok(files);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost("UploadImage")]
        public async Task<IActionResult> UploadFile(List<IFormFile> files, [FromForm] string id)
        {
            if(files.Count == 0)
                return BadRequest("No se adjuntó un archivo");

            var result = await driveRepository.UploadFile(files, id);

            return Ok(result);
        }

        [HttpPost("UploadCategoryFile")]
        public async Task<IActionResult> UploadCategoryFile(IFormFile file, [FromForm] string id)
        {
            if (files.Length == 0)
                return BadRequest("No se adjuntó un archivo");

            var result = await driveRepository.UploadCategoryFile(file, id);

            return Ok(result);
        }

        [HttpDelete("DeleteImage")]
        public async Task<IActionResult> DeleteFile(string fileId)
        {
            var result = await driveRepository.DeleteFile(fileId);

            return Ok(result);
        }
    }
}
