using ResturantRESTAPI.DTOs;

namespace ResturantRESTAPI.Services.IService
{
    public interface ITableService
    {
        Task<bool> AddTableAsync(TableDTO newTable);
        Task<bool> RemoveTableAsync(int tableId);
        Task<bool> UpdateTableAsync(int tableId, TableDTO newTable);
        Task<List<TableDTO>> GetAllTablesAsync();
        Task<TableDTO> GetTableByIdAsync(int tableId);
    }
}
