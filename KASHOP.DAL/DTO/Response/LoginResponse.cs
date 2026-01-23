using System;
using System.Collections.Generic;
using System.Text;

namespace KASHOP.DAL.DTO.Response
{
    public class LoginResponse
    {
        public string Message { get; set; }
        public bool Success { get; set; }
       
        public List<string>? Errors { get; set; }
        public string? AccessToken { get; set; }

    }
}
