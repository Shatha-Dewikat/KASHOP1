using KASHOP.DAL.validations;
using System;
using System.Collections.Generic;
using System.Text;

namespace KASHOP.DAL.DTO.Request
{
    public class ProductTranslationRequest
    {
        public string Name { get; set; }
        [MinValue(3)]
        public string Description { get; set; }

        public string Language { get; set; }

    }
}
