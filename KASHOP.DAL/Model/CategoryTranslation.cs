using System;
using System.Collections.Generic;
using System.Text;

namespace KASHOP.DAL.Model
{
    public class CategoryTranslation
    {
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public string Language { get; set; } = "en";
        public Category Category { get; set; }
        public int Id { get; set; }
    }
}
