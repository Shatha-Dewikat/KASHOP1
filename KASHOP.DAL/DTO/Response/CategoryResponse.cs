using KASHOP.DAL.DTO.Request;
using KASHOP.DAL.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace KASHOP.DAL.DTO.Response
{
    public class CategoryResponse
    {
        public readonly object CreatedBy;

        public Status Status { get; set; }
        public List<CategoryTranslationResponse> Translations { get; set; }
    }
}
