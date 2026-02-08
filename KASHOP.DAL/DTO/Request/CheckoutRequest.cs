using KASHOP.DAL.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace KASHOP.DAL.DTO.Request
{
    public class CheckoutRequest
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]

        public PaymentMethodEnum PaymentMethod { get; set; }

    }
}
