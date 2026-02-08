using KASHOP.BLL.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KASHOP.PL.Areas.Admin
{
    [Route("api/admin/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class ManagesController : ControllerBase
    {
        private readonly IManageUserService _ManageUserService;

        public ManagesController(IManageUserService ManageUserService)
        {
            _ManageUserService = ManageUserService;
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetUsers()
        {
            var result = await _ManageUserService.GetUsersAsync();
            return Ok(result);
        }

        [HttpPatch("block/{id}")]
        public async Task<IActionResult> BlockUser([FromRoute] string id)
    => Ok(await _ManageUserService.BlockedUserAsync(id));

        [HttpPatch("unblock/{id}")]
        public async Task<IActionResult> UnBlockUser([FromRoute] string id)
            => Ok(await _ManageUserService.UnBlockedUserAsync(id));

        [HttpPatch("change-role")]
        public async Task<IActionResult> ChangeRole(ChangeUserRoleRequest request)
    => Ok(await _ManageUserService.ChangeUserRoleAsync(request));



    }

}
