using Microsoft.EntityFrameworkCore;
using ResturantRESTAPI.Data;
using ResturantRESTAPI.Models;
using ResturantRESTAPI.Repositories.IRepositories;

namespace ResturantRESTAPI.Repositories
{
    public class TableRepository : ITableRepository
    {
        private readonly ResturantDbContext _context;
        
        public TableRepository(ResturantDbContext ctx)
        {
            _context = ctx;
        }

        public async Task<bool> AddTableAsync(Table newTable)
        {
            await _context.Tables.AddAsync(newTable);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<List<Table>> GetAllTablesAsync()
        {
            var allTables = await _context.Tables.ToListAsync();
            return allTables;
        }

        public async Task<Table> GetTableByIdAsync(int tableId)
        {
            var table = await _context.Tables.FirstOrDefaultAsync(t => t.Id == tableId);
            return table;
        }

        public async Task<bool> RemoveTableAsync(int tableId)
        {
            var table = await _context.Tables.FirstOrDefaultAsync(t => t.Id == tableId);
            if (table == null) return false;
            _context.Tables.Remove(table);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateTableCapacityAsync(int tableId, int newCapacity)
        {
            var table = await _context.Tables.FirstOrDefaultAsync(t => t.Id == tableId);
            if (table == null) return false;
            table.Capacity = newCapacity;
            _context.Tables.Update(table);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
