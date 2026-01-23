using System;
using System.Collections.Generic;
using System.Text;

namespace KASHOP.DAL.Model
{
    public class BaseModel
    {
        public int Id { get; set; }


        public Status status { get; set; }
        public DateTime CreatedAt { get; set; }


    }
}
