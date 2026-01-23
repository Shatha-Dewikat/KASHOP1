using KASHOP.BLL.Service;
using KASHOP.DAL.DTO.Request;
using KASHOP.DAL.Model;
using KASHOP.PL.Resourses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace KASHOP.PL.Areas.User
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _category;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public CategoriesController(
    ICategoryService category,
    IStringLocalizer<SharedResource> localizer
)
        {
            _category = category;
            _localizer = localizer;
            _localizer = localizer;
            _category = category;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            var response = _category.GetAllCategories();
            return Ok(new
            {
                message = _localizer["Success"].Value,
                response
            });
        }

        [HttpPost("")]
        public IActionResult Create(CategoryRequest request)
        {
            var response = _category.CreateCategory(request);
            return Ok(new
            {
                message = _localizer["Success"].Value
            });
        }


    }
}
