using ResturantRESTAPI.Models;

namespace ResturantRESTAPI.Repositories.IRepositories
{
    public interface IMenuItemRepository
    {
        Task<bool> AddMenuItemAsync(MenuItem newItem);
        Task<bool> RemoveMenuItemAsync(int itemId);
        Task<bool> UpdateMenuItemAsync(int itemId, MenuItem updatedItem);
        Task<MenuItem> GetMenuItemByIdAsync(int itemId);
        Task<List<MenuItem>> GetAllMenuItemsAsync();
    }
}
