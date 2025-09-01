using ResturantRESTAPI.Models;
using ResturantRESTAPI.Repositories.IRepositories;
using ResturantRESTAPI.Services.IService;

namespace ResturantRESTAPI.Services
{
    public class TableService : ITableService
    {
        private readonly ITableRepository _tableRepo;

        public TableService(ITableRepository repo)
        {
            _tableRepo = repo;
        }

        public Task<bool> AddTableAsync(Table newTable)
        {
            return _tableRepo.AddTableAsync(newTable);
        }

        public Task<List<Table>> GetAllTablesAsync()
        {
            return _tableRepo.GetAllTablesAsync();
        }

        public Task<Table> GetTableByIdAsync(int tableId)
        {
            return _tableRepo.GetTableByIdAsync(tableId);
        }

        public Task<bool> RemoveTableAsync(int tableId)
        {
            return _tableRepo.RemoveTableAsync(tableId);
        }

        public Task<bool> UpdateTableCapacityAsync(int tableId, int newCapacity)
        {
            return _tableRepo.UpdateTableCapacityAsync(tableId, newCapacity);
        }
    }
}
