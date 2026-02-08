using KASHOP.DAL.DTO.Response;
using KASHOP.DAL.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace KASHOP.DAL.Repository
{
    public interface IProductRepository
    {
        Task<Product> AddAsync(Product request);
       Task<Product> GetAllAsync();
        IQueryable<Product> Query();
        Task<bool> DecreaseQuantitesAsync(List<(int productId, int quantity)> items);

      

        Task<Product?>FindByIdAsync(int id);
    }
}
