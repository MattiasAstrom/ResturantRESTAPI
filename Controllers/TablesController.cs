using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ResturantRESTAPI.DTOs;
using ResturantRESTAPI.Services;
using ResturantRESTAPI.Services.IService;

namespace ResturantRESTAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class TablesController : ControllerBase
    {
        private readonly ITableService _tableService;

        public TablesController(ITableService service)
        {
            _tableService = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAvailableTables(DateTime startTime, int numberOfGuests)
        {
            var tables = await _tableService.GetAllTablesAsync();
            return Ok(tables);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTableById(int id)
        {
            var table = await _tableService.GetTableByIdAsync(id);
            if (table == null) return NotFound($"Table with ID {id} not found.");
            return Ok(table);
        }

        [HttpPost("create")]
        public async Task<IActionResult> AddTable([FromBody] TableDTO newTable)
        {
            if (newTable == null)
                return BadRequest("Invalid table data.");

            var success = await _tableService.AddTableAsync(newTable);

            if (!success)
                return StatusCode(500, "Failed to create table.");

            return CreatedAtAction(nameof(GetTableById), new { id = newTable.TableNumber }, newTable);
        }
        
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateTable(int id, [FromBody] TableDTO updatedTable)
        {
            if (updatedTable == null) 
                return BadRequest("Invalid table data.");

            var success = await _tableService.UpdateTableAsync(id, updatedTable);
            if (!success)
                return NotFound($"Table with ID {id} not found.");

            return Ok("Table updated successfully.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveTable(int id)
        {
            var success = await _tableService.RemoveTableAsync(id);
            if (!success)
                return NotFound($"Table with ID {id} not found.");

            return Ok("Table removed successfully.");
        }
    }
}
