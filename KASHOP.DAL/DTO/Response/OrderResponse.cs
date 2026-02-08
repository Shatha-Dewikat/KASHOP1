using KASHOP.DAL.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace KASHOP.DAL.DTO.Response
{
    public class OrderResponse
    {
        public int Id { get; set; }

        public OrderStatusEnum Status { get; set; }

        public string PaymentStatus { get; set; }

        public decimal AmountPaid { get; set; }

        public string userName { get; set; }
    }
}
