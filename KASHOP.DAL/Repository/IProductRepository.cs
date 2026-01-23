using KASHOP.DAL.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace KASHOP.DAL.Repository
{
    public interface IProductRepository
    {
        Task<Product> AddAsync(Product request);
    }
}
