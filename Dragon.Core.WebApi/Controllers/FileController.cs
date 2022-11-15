using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Dragon.Core.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IFileUploadService _fileUploadService;
        public FileController(IWebHostEnvironment hostEnvironment, IFileUploadService fileUploadService)
        {
            _hostEnvironment = hostEnvironment;
            _fileUploadService = fileUploadService;
        }
        [HttpGet("/sysfile/page")]
        public string GetFileList()
        {
            return "test";
        }
        [HttpPost("/sysfile/upload")]
        public async Task<bool> UploadFileAsunc([FromForm] FileFormDto fileFormDto)
        {
            string path = _hostEnvironment.WebRootPath;
            if (fileFormDto.File is not null)
            {
                foreach (IFormFile file in fileFormDto.File)
                {
                    await _fileUploadService.UploadFile(file, path);
                }

            }
            
            

            return true;
        }
    }
}
