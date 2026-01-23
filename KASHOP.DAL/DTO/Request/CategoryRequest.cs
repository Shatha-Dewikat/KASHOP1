using System;
using System.Collections.Generic;
using System.Text;

namespace KASHOP.DAL.DTO.Request
{
    public class CategoryRequest
    {
       public List<CategoryTranslationResponse> Translations { get; set; }
    }
}
