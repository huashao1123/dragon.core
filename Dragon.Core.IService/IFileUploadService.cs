using Dragon.Core.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dragon.Core.IService
{
    public interface IFileUploadService:IBaseService<SysFileItem>
    {
        Task<string> UploadFile(IFormFile file,string path);
        Task<bool> UpdateFileAsync(BaseFileDto baseFileDto, int id);
        Task<bool> UpdateFileAsync(int id, FileDto fileDto);
        Task<bool> AddFileDataAsync(SysFileItem sysFileItem);
        Task<PageModel<FileOutput>> GetPageListFileAsync(FilePageInput filePageInput);
    }
}
