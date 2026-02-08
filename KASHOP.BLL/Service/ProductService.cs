using KASHOP.DAL.DTO.Request;
using KASHOP.DAL.DTO.Response;
using KASHOP.DAL.Model;
using KASHOP.DAL.Repository;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace KASHOP.BLL.Service
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IFileService _fileService;

        public ProductService(
            IProductRepository productRepository,
            IFileService fileService)
        {
            _productRepository = productRepository;
            _fileService = fileService;
        }
        public async Task<ProductResponse> CreateProduct(ProductRequest request)
        {
            var product = request.Adapt<Product>();

            if (request.MainImage != null)
            {
                var imagePath = await _fileService.UploadAsync(request.MainImage);
                product.MainImage = imagePath;
            }

            await _productRepository.AddAsync(product);

            return product.Adapt<ProductResponse>();
        
        
        }
        public async Task<List<ProductUserResponse>> GetAllProductsForUser(string lang = "en", int page = 1, int limit = 3, string? search = null)
        {
            
            var query = _productRepository.Query();

            if (search is not null)
            {
                query = query.Where(p => p.Translations.Any(t => t.Language == lang && t.Name.Contains(search) || t.Description.Contains(search)));
            }

            var totalCount = await query.CountAsync();

            query = query.Skip((page - 1) * limit).Take(limit);
            var response = query.BuildAdapter()
                .AddParameters("Lang", lang)
                .AdaptToType<List<ProductUserResponse>>();

            return response;
        }

        public async Task<List<ProductUserResponse>> GetAllProductsForUser(string lang = "en")
        {
            var products = await _productRepository.GetAllAsync();

            var response = products
                .BuildAdapter()
                .AddParameters("lang", lang)
                .AdaptToType<List<ProductUserResponse>>();

            return response;
        }

        public async Task<List<ProductResponse>> GetAllProductsForAdmin()
        {
            var products = await _productRepository.GetAllAsync();

            var response = products.Adapt<List<ProductResponse>>();

            return response;
        }

        public async Task<List<ProductUserResponse>> GetAllProductsForUser(
    string lang = "en",
    int page = 1,
    int limit = 3)
        {
            var query = _productRepository.Query();

            var totalCount = await query.CountAsync();

            query = query.Skip((page - 1) * limit).Take(limit);

            var response = query
                .BuildAdapter()
                .AddParameters("lang", lang)
                .AdaptToType<List<ProductUserResponse>>();

            return response;
        }
        public async Task<ProductUserDetails> GetAllProductsDetailsForUser(int id,
       string lang = "en")
        {
            var product = await _productRepository.FindByIdAsync(id);

            var response = product
                .BuildAdapter()
                .AddParameters("lang", lang)
                .AdaptToType<ProductUserDetails>();

            return response;
        }

      
    }
}
