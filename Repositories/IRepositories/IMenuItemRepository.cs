using ResturantRESTAPI.DTOs;
using ResturantRESTAPI.Models;

namespace ResturantRESTAPI.Repositories.IRepositories
{
    public interface IMenuItemRepository
    {
        Task<bool> AddMenuItemAsync(MenuItemDTO newItem);
        Task<bool> RemoveMenuItemAsync(int itemId);
        Task<bool> UpdateMenuItemAsync(int itemId, MenuItemDTO updatedItem);
        Task<MenuItemDTO> GetMenuItemByIdAsync(int itemId);
        Task<List<MenuItemDTO>> GetAllMenuItemsAsync();
    }
}
