using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hiq_coding_test.Services
{
    public interface IFileUploadService
    {
        bool CheckIfAcceptedFileType(IFormFile file);
    }
}
