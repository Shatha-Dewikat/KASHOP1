using KASHOP.BLL.MapsterConfigration;
using KASHOP.DAL.DTO.Request;
using KASHOP.DAL.DTO.Response;
using KASHOP.DAL.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace KASHOP.BLL.Service
{
    public interface ICategoryService
    {
        Task<List<CategoryUserResponse>> GetAllCategoriesForUser(string lang = "en");
        Task<List<CategoryResponse>> GetAllCategoriesForAdmin();
        CategoryResponse CreateCategory(CategoryRequest Request);

        Task<BaseResponse> DeleteCategoryAsync(int id);
        Task<BaseResponse> UpdateCategoryAsync(int id, CategoryRequest request);
    }
}
