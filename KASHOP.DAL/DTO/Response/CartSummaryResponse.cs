using System;
using System.Collections.Generic;
using System.Text;

namespace KASHOP.DAL.DTO.Response
{
    public class CartSummaryResponse
    {
        public List<CartResponse> Items { get; set; }

        public decimal CartTotal => Items.Sum(i => i.TotalPrice);

    }
}
