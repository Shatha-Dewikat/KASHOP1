using System;
using System.Collections.Generic;
using System.Text;

namespace KASHOP.DAL.Model
{
    public class Category : BaseModel
    {

        public string? CreatedBy { get; set; }




        public ApplicationUser User { get; set; }
        public List<CategoryTranslation> Translations { get; set; }
        public List<Product> Products { get; set; }

    }
}
