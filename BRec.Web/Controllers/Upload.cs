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
    public class Upload : Controller
    {
        IWebHostEnvironment _hostingEnvironment;

        public Upload(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;

        }

        [Route("api/[controller]/Save")]
        [HttpPost]
        public async Task<IActionResult> Save(IFormFile file)
        {
            //if (file.ContentType != "audio/wav")
            //{
            //    return BadRequest("Wrong file type");
            //}
            var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");//uploads where you want to save data inside wwwroot

            //var filePath = Path.Combine(uploads, Path.GetRandomFileName() + ".wav");
            var filePath = Path.Combine(uploads, file.FileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
            return Ok("File uploaded successfully");
        }
    }
}
