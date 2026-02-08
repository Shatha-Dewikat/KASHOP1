using KASHOP.DAL.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace KASHOP.DAL.DTO.Response
{
    public class ProductUserResponse
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
public string Name { get; set; }

        public int Quantity { get; set; }

        public double Rate { get; set; }

        public string MainImage { get; set; }

        

    }
}
