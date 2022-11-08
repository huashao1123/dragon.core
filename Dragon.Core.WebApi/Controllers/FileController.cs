using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Dragon.Core.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        [HttpGet("sysfile/getlist")]
        public string GetFileList()
        {
            return "test";
        }
        [HttpPost("sysfile/add")]
        public async Task<bool> AddFileAsunc()
        {
            return true;
        }
    }
}
