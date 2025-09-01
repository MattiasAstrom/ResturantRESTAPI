using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ResturantRESTAPI.DTOs;
using ResturantRESTAPI.Models;
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
        [Authorize(Roles = "Admin")]
        public IActionResult AddMenuItem([FromBody] MenuItemDTO menuItem)
        {
            if (menuItem == null)
            {
                return BadRequest("Menu item is null.");
            }
            var createdMenuItem = _menuItemService.AddMenuItemAsync(menuItem);
            return CreatedAtAction(nameof(GetAllMenuItems), new { id = createdMenuItem.Id }, createdMenuItem);
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteMenuItem(int id)
        {
            if (!_menuItemService.RemoveMenuItemAsync(id).Result)
            {
                return NotFound($"Menu item with id {id} not found.");
            }
            return Ok("Menu item Deleted successfully.");
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult UpdateMenuItemAsync(int menuItemId, MenuItemDTO updatedMenuItem)
        {
            var updated = _menuItemService.UpdateMenuItemAsync(menuItemId, updatedMenuItem).Result;
            if (!updated)
            {
                return NotFound($"Menu item with id {menuItemId} not found.");
            }
            return Ok("Menu item updated successfully.");
        }
    }
}

