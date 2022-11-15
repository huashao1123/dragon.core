using Dragon.Core.IService;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dragon.Core.Service
{
    public class FileUploadService : IFileUploadService
    {
        public FileUploadService()
        {

        }
        public async Task<string> UploadFile(IFormFile file,string path)
        {
            string savePath = $"{path}/上传文件/";
            if (!Directory.Exists(savePath))
            {
                Directory.CreateDirectory(savePath);
            }
            string saveFileName = Path.Combine(savePath,file.FileName);
            using (var stream = File.Create(saveFileName))
            {
                await file.CopyToAsync(stream);
            }
            return file.FileName;
        }
    }
}
