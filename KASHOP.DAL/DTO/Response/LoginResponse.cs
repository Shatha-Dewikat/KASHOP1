using System;
using System.Collections.Generic;
using System.Text;

namespace KASHOP.DAL.DTO.Response
{
    public class LoginResponse : BaseResponse
    {
       
        public string? AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
