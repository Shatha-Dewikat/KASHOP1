using System;
using System.Collections.Generic;
using System.Text;

namespace KASHOP.BLL.Service
{
    public class ChangeUserRoleRequest
    {
        public string UserId { get; set; }
        public string Role { get; set; }
    }
}
