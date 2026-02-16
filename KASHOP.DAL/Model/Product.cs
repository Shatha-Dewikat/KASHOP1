using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace KASHOP.DAL.Model
{
    public class Product : BaseModel
    {
        public decimal Price { get; set; }

        public decimal Discount { get; set; }

        public int Quantity { get; set; }

        public double Rate { get; set; }

        public string? MainImage { get; set; }

        public int CategoryId { get; set; }
        public string? Description { get; set; }
        public Category ?Category { get; set; }

        public List<ProductTranslation> Translations { get; set; }

       

        public List<Review> Reviews { get; set; }



    }
}
