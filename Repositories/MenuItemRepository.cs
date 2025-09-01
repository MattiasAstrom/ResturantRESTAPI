using Microsoft.EntityFrameworkCore;
using ResturantRESTAPI.Data;
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

        public async Task<bool> AddMenuItemAsync(MenuItem newItem)
        {
            if (newItem == null) return false;

            await _context.MenuItems.AddAsync(newItem);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<MenuItem>> GetAllMenuItemsAsync()
        {
            var allItems = await _context.MenuItems.ToListAsync();
            return allItems;
        }

        public async Task<MenuItem> GetMenuItemByIdAsync(int itemId)
        {
            var item = await _context.MenuItems.FirstOrDefaultAsync(i => i.Id == itemId);
            return item;
        }

        public async Task<bool> RemoveMenuItemAsync(int itemId)
        {
            var item = await _context.MenuItems.FirstOrDefaultAsync(i => i.Id == itemId);
            if (item == null) return false;
            _context.MenuItems.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateMenuItemAsync(int itemId, MenuItem updatedItem)
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
