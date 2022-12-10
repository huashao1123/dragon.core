using Dragon.Core.Entity;
using Dragon.Core.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Dragon.Core.WebApi.Controllers
{
    [Authorize(Permissions.Name)]
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IFileUploadService _fileUploadService;
        private readonly IUser _user;
        public FileController(IWebHostEnvironment hostEnvironment, IFileUploadService fileUploadService, IUser user)
        {
            _hostEnvironment = hostEnvironment;
            _fileUploadService = fileUploadService;
            _user = user;
        }
        [HttpGet("/sysfile/page")]
        public async Task<PageModel<FileOutput>> GetFileList([FromQuery] FilePageInput filePageInput)
        {
            var pageList = await _fileUploadService.GetPageListFileAsync(filePageInput);
            return pageList;
        }
        [HttpPost("/sysfile/upload")]
        public async Task<bool> UploadFileAsunc([FromForm] FileFormDto fileFormDto)
        {

            int id = fileFormDto.id;
            string path = _hostEnvironment.WebRootPath;
            var files = fileFormDto.File;
            if (id>0)
            {
                if (files?.Count>1)
                {
                    throw new UserFriendlyException("上传了多个文件");
                }
                //files?.ForEach( async (file) =>
                //{
                //    await _fileUploadService.UploadFile(file, path);
                //});
                var file=files?.FirstOrDefault();

                if (file==null)
                {
                    BaseFileDto baseFileDto=new BaseFileDto() { 
                        fileLevel= (FileLevelEnum)fileFormDto.fileLevel, 
                        fileVersion=fileFormDto.fileVersion,
                };
                    return await _fileUploadService.UpdateFileAsync(baseFileDto, id);
                }
                else
                {
                    string saveFileName = await _fileUploadService.UploadFile(file, path);
                    long len = file.Length;
                    string fileName = Path.GetFileName(file.FileName);
                    string fileExtension = Path.GetExtension(file.FileName);
                    FileDto sysFileItem = new FileDto()
                    {
                        FileSizeInBytes = len,
                        FileName = fileName,
                        Suffix = fileExtension,
                        fileVersion = fileFormDto.fileVersion,
                        fileLevel = (FileLevelEnum)fileFormDto.fileLevel,
                        FilePath= saveFileName,
                    };
                    return await _fileUploadService.UpdateFileAsync(id, sysFileItem);
                }
            }
            else
            {
                _=files ?? throw new UserFriendlyException("未上传文件");
                foreach (IFormFile file in files)
                {
                    string saveFileName = await _fileUploadService.UploadFile(file, path);
                    long len = file.Length;
                    string fileName = Path.GetFileName(file.FileName);
                    string fileExtension = Path.GetExtension(file.FileName);
                    SysFileItem sysFileItem = new SysFileItem()
                    {
                        FileSizeInBytes= len,
                        FileName = fileName,
                        Suffix= fileExtension,
                        FileVersion=fileFormDto.fileVersion,
                        FileLevel=(FileLevelEnum)fileFormDto.fileLevel,
                        FilePath=saveFileName,
                    };
                    await _fileUploadService.AddFileDataAsync(sysFileItem);
                }
            }
            return true;
        }
        [HttpDelete("/sysfile/delete")]
        public async Task<bool> DeleteFileAsync([FromBody]int id)
        {
            await _fileUploadService.DeleteAsync(d => d.Id == id);
            return true;
        }
        [HttpPost("/sysfile/download")]
        public async Task<IActionResult>DownLoadFileAsync([FromBody] int id)
        {
            var file = await _fileUploadService.GetEntityAsync(id);
            if (file==null)
            {
                return NotFound("未找到文件");
            }
            string path = _hostEnvironment.WebRootPath;
            string fileName = Path.Combine($"{path}/Upload/", file.FileName);
            if (!System.IO.File.Exists(fileName))
            {
                return NotFound("未找到文件");
            }
            return new FileStreamResult(new FileStream(fileName, FileMode.Open), "application/octet-stream") { FileDownloadName = file.FileName };
        }
    }
}
