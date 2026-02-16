using KASHOP.DAL.DTO.Request;
using KASHOP.DAL.DTO.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace KASHOP.BLL.Service
{
    public interface IProductService
    {
        Task<ProductResponse> CreateProduct(ProductRequest request);

        Task<List<ProductResponse>> GetAllProductsForAdmin();
        Task<List<ProductUserResponse>> GetAllProductsForUser(string lang = "en", int page = 1, int limit = 3, string? search = null);
        Task<List<ProductUserResponse>> GetAllProductsForUser(string lang = "en");
      
        Task<ProductUserDetails> GetAllProductsDetailsForUser(int id,
       string lang = "en");
    }
}
