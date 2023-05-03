using Microsoft.AspNetCore.Mvc;
using Restaurant_Tool_API.Models;
using Restaurant_Tool_API.Services;

namespace Restaurant_Tool_API.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
[Produces("application/json")]
public class TablesController : ControllerBase
{
    private readonly IDataService _dataService;
    public TablesController(IDataService dataService)
    {
        _dataService = dataService;
    }

    /// <summary>
    /// Get all tables.
    /// </summary>
    /// <response code="200">Returns tables.</response>
    /// <response code="404">If the tables are null.</response>
    [HttpGet]
    [ProducesResponseType(typeof(List<Tables>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetTables()
    {
        var result = _dataService.GetTablesAsync();

        if (result == null) return this.NotFound("No Tables");

        return this.Ok(result);
    }
}
