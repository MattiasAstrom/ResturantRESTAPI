using Microsoft.EntityFrameworkCore;
using ResturantRESTAPI.Data;
using ResturantRESTAPI.DTOs;
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

        public async Task<bool> AddTableAsync(TableDTO newTable)
        {
            if (newTable == null) return false;

            var table = new Table
            {
                TableNumber = newTable.TableNumber,
                Capacity = newTable.Capacity
            };
            await _context.Tables.AddAsync(table);

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<List<TableDTO>> GetAllTablesAsync()
        {
            var allTables = await _context.Tables.ToListAsync();
            List<TableDTO> tableDTOs = new List<TableDTO>();
            foreach (var item in allTables)
            {
                var temp = new TableDTO
                {
                    TableNumber = item.TableNumber,
                    Capacity = item.Capacity
                };
                tableDTOs.Add(temp);
            }
            return tableDTOs;
        }

        public async Task<TableDTO> GetTableByIdAsync(int tableId)
        {
            var table = await _context.Tables.FirstOrDefaultAsync(t => t.Id == tableId);
            if (table == null) return null;
            var tableDTO = new TableDTO
            {
                TableNumber = table.TableNumber,
                Capacity = table.Capacity
            };
            return tableDTO;
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
