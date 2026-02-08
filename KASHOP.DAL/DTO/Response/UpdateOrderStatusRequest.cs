using KASHOP.DAL.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace KASHOP.DAL.DTO.Response
{
    public class UpdateOrderStatusRequest
    {
        public OrderStatusEnum Status { get; set; }
    }
}
