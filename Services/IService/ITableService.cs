using ResturantRESTAPI.DTOs;

namespace ResturantRESTAPI.Services.IService
{
    public interface ITableService
    {
        Task<bool> AddTableAsync(TableDTO newTable);
        Task<bool> RemoveTableAsync(int tableId);
        Task<bool> UpdateTableCapacityAsync(int tableId, int newCapacity);
        Task<List<TableDTO>> GetAllTablesAsync();
        Task<TableDTO> GetTableByIdAsync(int tableId);
    }
}
