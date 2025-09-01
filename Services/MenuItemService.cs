using ResturantRESTAPI.Models;
using ResturantRESTAPI.Repositories.IRepositories;
using ResturantRESTAPI.Services.IService;

namespace ResturantRESTAPI.Services
{
    public class MenuItemService : IMenuItemService
    {
        private readonly IMenuItemRepository _menuItemRepo;
        public MenuItemService(IMenuItemRepository menuItemRepo)
        {
            _menuItemRepo = menuItemRepo;           
        }

        public Task<bool> AddMenuItemAsync(MenuItem newMenuItem)
        {
            return _menuItemRepo.AddMenuItemAsync(newMenuItem);
        }

        public Task<List<MenuItem>> GetAllMenuItemsAsync()
        {
            return _menuItemRepo.GetAllMenuItemsAsync();
        }

        public Task<MenuItem> GetMenuItemByIdAsync(int menuItemId)
        {
            return _menuItemRepo.GetMenuItemByIdAsync(menuItemId);
        }

        public Task<bool> RemoveMenuItemAsync(int menuItemId)
        {
            return _menuItemRepo.RemoveMenuItemAsync(menuItemId);
        }

        public Task<bool> UpdateMenuItemAsync(int menuItemId, MenuItem updatedMenuItem)
        {
            return _menuItemRepo.UpdateMenuItemAsync(menuItemId, updatedMenuItem);
        }
    }
}
