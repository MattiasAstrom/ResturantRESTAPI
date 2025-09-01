using ResturantRESTAPI.DTOs;
using ResturantRESTAPI.Models;

namespace ResturantRESTAPI.Services.IService
{
    public interface IMenuItemService
    {
        Task<bool> AddMenuItemAsync(MenuItemDTO newMenuItem);
        Task<bool> RemoveMenuItemAsync(int menuItemId);
        Task<bool> UpdateMenuItemAsync(int menuItemId, MenuItemDTO updatedMenuItem);
        Task<List<MenuItemDTO>> GetAllMenuItemsAsync();
        Task<MenuItemDTO> GetMenuItemByIdAsync(int menuItemId);
    }
}
