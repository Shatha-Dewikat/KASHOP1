using KASHOP.DAL.DTO.Request;
using KASHOP.DAL.DTO.Response;
using KASHOP.DAL.Model;
using KASHOP.DAL.Repository;
using Mapster;
using System;
using System.Collections.Generic;
using System.Text;

namespace KASHOP.BLL.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRespository _categoryRepository;

        public CategoryService(ICategoryRespository categoryRespository)
        {
            _categoryRepository = categoryRespository;
        }
        public CategoryResponse CreateCategory(CategoryRequest Request)
        {
            var category = Request.Adapt<Category>();
            _categoryRepository.Create(category);

            return category.Adapt<CategoryResponse>();

        }

        public List<CategoryResponse> GetAllCategories()
        {
            var categories = _categoryRepository.GetAll();
            var response = categories.Adapt<List<CategoryResponse>>();
            return response;

        }
    }
}
