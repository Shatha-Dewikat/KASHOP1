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
        List<CategoryResponse> GetAllCategories();
        CategoryResponse CreateCategory(CategoryRequest Request);
    }
}
