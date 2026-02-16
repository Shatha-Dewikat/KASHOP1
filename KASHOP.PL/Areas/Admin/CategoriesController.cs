using KASHOP.BLL.Service;
using KASHOP.DAL.DTO.Request;
using KASHOP.DAL.Model;
using KASHOP.PL.Resourses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace KASHOP.PL.Areas.Admin
{
    [Route("api/admin/[controller]")]
    [ApiController]
    [Authorize]
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

      


        [HttpPost("")]
        public IActionResult Create(CategoryRequest request)
        {
            var response = _category.CreateCategory(request);
            
            return Ok(new
            {
                message = _localizer["Success"].Value
            });
        }

      

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory([FromRoute] int id)
        {
            var result = await _category.DeleteCategoryAsync(id);

            if (!result.Success)
            {
                if (result.Message.Contains("Not Found"))
                {
                    return NotFound(result);
                }

                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            var response = await _category.GetAllCategoriesForAdmin();
            return Ok(new { message = _localizer["Success"].Value, response });
        }


        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateCategory(
    [FromRoute] int id,
    [FromBody] CategoryRequest request)
        {
            var result = await _category.UpdateCategoryAsync(id, request);

            if (!result.Success)
            {
                if (result.Message.Contains("Not Found"))
                {
                    return NotFound(result);
                }

                return BadRequest(result);
            }

            return Ok(result);
        }



    }
}
