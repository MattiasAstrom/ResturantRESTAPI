using ResturantRESTAPI.Models;

namespace ResturantRESTAPI.Services.IService
{
    public interface ITableService
    {
        Task<bool> AddTableAsync(Table newTable);
        Task<bool> RemoveTableAsync(int tableId);
        Task<bool> UpdateTableCapacityAsync(int tableId, int newCapacity);
        Task<List<Table>> GetAllTablesAsync();
        Task<Table> GetTableByIdAsync(int tableId);
    }
}
