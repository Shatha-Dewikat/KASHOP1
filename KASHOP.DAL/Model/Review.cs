using System;
using System.Collections.Generic;
using System.Text;

namespace KASHOP.DAL.Model
{
    public class Review
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product product { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public int Rating { get; set; }
        public string Comment { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

}
