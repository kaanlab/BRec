using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BRec.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        IWebHostEnvironment _hostingEnvironment;

        public UploadController(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;

        }

        [HttpPost("save")]
        public async Task<IActionResult> Save(IFormFile uploadfile)
        {
            if (uploadfile == null)
                return BadRequest("File is empty");

            if (uploadfile.ContentType != "au" && !uploadfile.FileName.EndsWith("mp3"))            
                return BadRequest("Wrong file type");          

            var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");//uploads where you want to save data inside wwwroot
            if (!Directory.Exists(uploads))
                return BadRequest("Directory does not exist");
            
            //var filePath = Path.Combine(uploads, Path.GetRandomFileName() + ".wav");
            var filePath = Path.Combine(uploads, uploadfile.FileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await uploadfile.CopyToAsync(fileStream);
            }
            return Ok("File uploaded successfully");
        }

        [HttpGet("values")]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
    }
}
