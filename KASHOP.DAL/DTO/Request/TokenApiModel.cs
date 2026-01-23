using System;
using System.Collections.Generic;
using System.Text;

namespace KASHOP.DAL.DTO.Request
{
    public class TokenApiModel
    {
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
    }
}
