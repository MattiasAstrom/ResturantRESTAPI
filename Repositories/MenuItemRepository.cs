using Microsoft.EntityFrameworkCore;
using ResturantRESTAPI.Data;
using ResturantRESTAPI.DTOs;
using ResturantRESTAPI.Models;
using ResturantRESTAPI.Repositories.IRepositories;

namespace ResturantRESTAPI.Repositories
{
    public class MenuItemRepository : IMenuItemRepository
    {
        private readonly ResturantDbContext _context;
        public MenuItemRepository(ResturantDbContext ctx)
        {
            _context = ctx;
        }

        public async Task<bool> AddMenuItemAsync(MenuItemDTO newItem)
        {
            if (newItem == null) return false;
            var item = new MenuItem
            {
                Name = newItem.Name,
                Description = newItem.Description,
                Price = newItem.Price,
                IsPopular = newItem.IsPopular,
                ImageUrl = newItem.ImageUrl
            };

            await _context.MenuItems.AddAsync(item);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<MenuItemDTO>> GetAllMenuItemsAsync()
        {
            var allItems = await _context.MenuItems.ToListAsync();
            List<MenuItemDTO> allItemsDto = new List<MenuItemDTO>();
            foreach (var item in allItems)
            {
                allItemsDto.Add(new MenuItemDTO
                {
                    Name = item.Name,
                    Description = item.Description,
                    Price = item.Price,
                    IsPopular = item.IsPopular,
                    ImageUrl = item.ImageUrl
                });
            }
            return allItemsDto;
        }

        public async Task<MenuItemDTO> GetMenuItemByIdAsync(int itemId)
        {
            var item = await _context.MenuItems.FirstOrDefaultAsync(i => i.Id == itemId);
            if (item == null) return null;
            var itemDto = new MenuItemDTO
            {
                Name = item.Name,
                Description = item.Description,
                Price = item.Price,
                IsPopular = item.IsPopular,
                ImageUrl = item.ImageUrl
            };
            return itemDto;
        }

        public async Task<bool> RemoveMenuItemAsync(int itemId)
        {
            var item = await _context.MenuItems.FirstOrDefaultAsync(i => i.Id == itemId);
            if (item == null) return false;
            _context.MenuItems.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateMenuItemAsync(int itemId, MenuItemDTO updatedItem)
        {
            var item = await _context.MenuItems.FirstOrDefaultAsync(i => i.Id == itemId);

            if (item == null) return false;
            
            item.Name = updatedItem.Name;
            item.Description = updatedItem.Description;
            item.Price = updatedItem.Price;
            item.IsPopular = updatedItem.IsPopular;
            item.ImageUrl = updatedItem.ImageUrl;
            
            _context.MenuItems.Update(item);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
