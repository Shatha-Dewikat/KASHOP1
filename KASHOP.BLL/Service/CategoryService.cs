using KASHOP.BLL.MapsterConfigration;
using KASHOP.DAL.DTO.Request;
using KASHOP.DAL.DTO.Response;
using KASHOP.DAL.Model;
using KASHOP.DAL.Repository;
using Mapster;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;


namespace KASHOP.BLL.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRespository _categoryRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CategoryService(ICategoryRespository categoryRespository, IHttpContextAccessor httpContextAccessor)
        {
            _categoryRepository = categoryRespository;
            _httpContextAccessor = httpContextAccessor;
        }
        public CategoryResponse CreateCategory(CategoryRequest Request)
        {

            var category = Request.Adapt<Category>();
           

            _categoryRepository.Create(category);

            return category.Adapt<CategoryResponse>();

        }

        public async Task<List<CategoryResponse>> GetAllCategoriesForAdmin()
        {
            var categories = _categoryRepository.GetAll();

            var response = categories.Adapt<List<CategoryResponse>>();
            return response;
        }

        public async Task<List<CategoryUserResponse>> GetAllCategoriesForUser(string lang = "en")
        {
            var categories =  _categoryRepository.GetAll();

            foreach (var category in categories)
            {
                category.Translations = category.Translations
                    .Where(t => t.Language == lang)
                    .ToList();
            }

            var response = categories.Adapt<List<CategoryUserResponse>>();
            return response;
        }


        public async Task<BaseResponse> DeleteCategoryAsync(int id)
        {
            try
            {
                var category = await _categoryRepository.FindByIdAsync(id);

                if (category is null)
                {
                    return new BaseResponse
                    {
                        Success = false,
                        Message = "Category Not Found"
                    };
                }

                await _categoryRepository.DeleteAsync(category);

                return new BaseResponse
                {
                    Success = true,
                    Message = "Category Deleted Successfully"
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<BaseResponse> UpdateCategoryAsync(int id, CategoryRequest request)
        {
            try
            {
                var category = await _categoryRepository.FindByIdAsync(id);

                if (category is null)
                {
                    return new BaseResponse
                    {
                        Success = false,
                        Message = "Category Not Found"
                    };
                }

                if (request.Translations != null)
                {
                    foreach (var translation in request.Translations)
                    {
                        var existing = category.Translations
                            .FirstOrDefault(t => t.Language == translation.Language);

                        if (existing is not null)
                        {
                            existing.Name = translation.Name;
                        }
                        else
                        {
                            category.Translations.Add(new CategoryTranslation
                            {
                                Name = translation.Name,
                                Language = translation.Language
                            });
                        }
                    }
                }

                category.Translations = request.Translations
                    .Adapt<List<CategoryTranslation>>();

                await _categoryRepository.UpdateAsync(category);

                return new BaseResponse
                {
                    Success = true,
                    Message = "Category Updated Successfully"
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }


    }
}
