using ResturantRESTAPI.Models;

namespace ResturantRESTAPI.Services.IService
{
    public interface IMenuItemService
    {
        Task<bool> AddMenuItemAsync(MenuItem newMenuItem);
        Task<bool> RemoveMenuItemAsync(int menuItemId);
        Task<bool> UpdateMenuItemAsync(int menuItemId, MenuItem updatedMenuItem);
        Task<List<MenuItem>> GetAllMenuItemsAsync();
        Task<MenuItem> GetMenuItemByIdAsync(int menuItemId);
    }
}
