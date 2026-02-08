using System;
using System.Collections.Generic;
using System.Text;

namespace KASHOP.DAL.DTO.Response
{
    public class CheckoutResponse : BaseResponse
    {
        public string? Url { get; set; }

        public string? PaymentId { get; set; }

    }
}
