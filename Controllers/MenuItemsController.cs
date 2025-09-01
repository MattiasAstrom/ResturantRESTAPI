using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ResturantRESTAPI.Services.IService;

namespace ResturantRESTAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuItemsController : ControllerBase
    {
        private readonly IMenuItemService _menuItemService;
        public MenuItemsController(IMenuItemService service)
        {
            _menuItemService = service;
        }

        [HttpGet]
        public IActionResult GetAllMenuItems()
        {
            var menuItems = _menuItemService.GetAllMenuItemsAsync();
            return Ok(menuItems);
        }

        //Admin stuff
        [HttpPost]
        public IActionResult AddMenuItem([FromBody] Models.MenuItem menuItem)
        {
            if (menuItem == null)
            {
                return BadRequest("Menu item is null.");
            }
            var createdMenuItem = _menuItemService.AddMenuItemAsync(menuItem);
            return CreatedAtAction(nameof(GetAllMenuItems), new { id = createdMenuItem.Id }, createdMenuItem);
        }
        [HttpDelete]
        public IActionResult DeleteMenuItem(int id)
        {
            if (!_menuItemService.RemoveMenuItemAsync(id).Result)
            {
                return NotFound($"Menu item with id {id} not found.");
            }
            return NoContent();
        }
    }
}

