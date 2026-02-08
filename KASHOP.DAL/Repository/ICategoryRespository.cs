using KASHOP.DAL.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace KASHOP.DAL.Repository
{
    public interface ICategoryRespository
    {
        List<Category> GetAll();
        Category Create(Category Request);

    }
}
