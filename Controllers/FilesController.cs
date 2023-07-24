using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using System.IO;
using System.Net;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Configuration;

namespace InfoCity.API.Controllers
{

    [Route("api/archivos")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly FileExtensionContentTypeProvider _fileExtension;
        private readonly IConfiguration _configuration;

        public FilesController(FileExtensionContentTypeProvider file, IConfiguration configuration)
        {
            _fileExtension = file
            ?? throw new ArgumentNullException(nameof(file));
            _configuration = configuration;
        }

            [HttpGet("{assetid}")]
        public ActionResult GetFile(string assetid)
        {

            Cloudinary cloudinary = new Cloudinary(_configuration["ConnectionCludinaryStrings:key"]);
            var result = cloudinary.GetResourceByAssetId("993e865d7fa0c38e19eaa4c4b0ed495f");
            Stream remoteStream = null;
            WebResponse response = null;
            if (result.StatusCode == System.Net.HttpStatusCode.BadRequest){
                return NotFound();
            }
            WebRequest request = WebRequest.Create(result.SecureUrl);
            byte[] byteArray= new byte[1024];
            var pathToFile = result.PublicId +"."+ result.Format;
            if (request != null)
            {
                var sizewebrequest = HttpWebRequest.Create(result.SecureUrl);
                sizewebrequest.Method = "HEAD";
                response = request.GetResponse();
                
                if(response != null)
                {
                    remoteStream = response.GetResponseStream();
                    MemoryStream ms = new MemoryStream();
                    remoteStream.CopyTo(ms);
                    byteArray = ms.ToArray();
                }
            }
            if(!_fileExtension.TryGetContentType(pathToFile, out string contentType))
            {
                contentType = "application/octet-stream";
            }
            return File(byteArray, contentType, Path.GetFileName(pathToFile));
        }
    }
}
