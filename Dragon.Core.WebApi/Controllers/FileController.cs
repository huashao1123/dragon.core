using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Dragon.Core.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        [HttpGet("/sysfile/page")]
        public string GetFileList()
        {
            return "test";
        }
        [HttpPost("/sysfile/add")]
        public async Task<bool> AddFileAsunc()
        {
            return true;
        }
    }
}
