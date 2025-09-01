using ResturantRESTAPI.DTOs;
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

        public Task<bool> AddMenuItemAsync(MenuItemDTO newMenuItem)
        {
            return _menuItemRepo.AddMenuItemAsync(newMenuItem);
        }

        public Task<List<MenuItemDTO>> GetAllMenuItemsAsync()
        {
            return _menuItemRepo.GetAllMenuItemsAsync();
        }

        public Task<MenuItemDTO> GetMenuItemByIdAsync(int menuItemId)
        {
            return _menuItemRepo.GetMenuItemByIdAsync(menuItemId);
        }

        public Task<bool> RemoveMenuItemAsync(int menuItemId)
        {
            return _menuItemRepo.RemoveMenuItemAsync(menuItemId);
        }

        public Task<bool> UpdateMenuItemAsync(int menuItemId, MenuItemDTO updatedMenuItem)
        {
            return _menuItemRepo.UpdateMenuItemAsync(menuItemId, updatedMenuItem);
        }
    }
}
