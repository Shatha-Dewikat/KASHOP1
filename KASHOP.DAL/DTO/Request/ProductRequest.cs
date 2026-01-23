using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace KASHOP.DAL.DTO.Request
{
    public class ProductRequest
    {
        public readonly object MainImage;

        public List<ProductTranslationRequest> Translations { get; set; }

        public decimal Price { get; set; }

        public decimal Discount { get; set; }

        public int Quantity { get; set; }

      public String Image { get; set; }

        public int CategoryId { get; set; }
    }
}
