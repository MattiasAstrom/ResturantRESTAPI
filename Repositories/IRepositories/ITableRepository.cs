using ResturantRESTAPI.DTOs;
using ResturantRESTAPI.Models;

namespace ResturantRESTAPI.Repositories.IRepositories
{
    public interface ITableRepository
    {
        Task<bool> AddTableAsync(TableDTO newTable);
        Task<bool> RemoveTableAsync(int tableId);
        Task<bool> UpdateTableAsync(int tableId, TableDTO newTable);
        Task<List<TableDTO>> GetAllTablesAsync();
        Task<TableDTO> GetTableByIdAsync(int tableId);
    }
}
