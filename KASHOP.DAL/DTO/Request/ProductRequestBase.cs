using Microsoft.AspNetCore.Http;

namespace KASHOP.DAL.DTO.Request
{
    public class ProductRequestBase
    {

        public IFormFile? MainImage { get; set; }
    }
}