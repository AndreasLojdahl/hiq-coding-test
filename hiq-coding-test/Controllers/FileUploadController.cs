using hiq_coding_test.Models;
using hiq_coding_test.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hiq_coding_test.Controllers
{
    [ApiController]
    [Route("api/")]
    public class FileUploadController: ControllerBase
    {
        private readonly IFileUploadService _fileUploadService;

        public FileUploadController(IFileUploadService fileUploadService)
        {
            _fileUploadService = fileUploadService;
        }

        [HttpPost("fileUpload")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [Consumes("multipart/form-data")]
        public IActionResult UploadFile([FromForm] IFormFile file)
        {
         
            {
                var isAcceptedFile = _fileUploadService.CheckIfAcceptedFileType(file);
                Console.WriteLine(isAcceptedFile);
                Console.WriteLine(file.FileName);
                if (isAcceptedFile)
                {
                    return Ok();
                }
                else
                {
                    return new StatusCodeResult(StatusCodes.Status400BadRequest);
                }
            }
            try
            {
                return null;
                
            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status400BadRequest);

            }
        }

  
    }
}
