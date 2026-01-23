using KASHOP.BLL.Service;
using KASHOP.DAL.DTO.Request;
using KASHOP.PL.Resourses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace KASHOP.PL.Areas.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public ProductsController(
            IProductService productService,
            IStringLocalizer<SharedResource> localizer
        )
        {
            _productService = productService;
            _localizer = localizer;
        }

        [HttpPost("")]
        public async Task<IActionResult> Create([FromForm] ProductRequest request)
        {
            var response = await _productService.CreateProduct(request);
            return Ok(response);
        }

    }
}
