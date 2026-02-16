using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace KASHOP.BLL.Service
{
    public interface IFileService
    {
        Task<string?> UploadAsync(IFormFile file);
        
    }
}
