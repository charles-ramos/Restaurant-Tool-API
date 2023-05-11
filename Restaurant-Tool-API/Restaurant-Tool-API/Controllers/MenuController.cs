using Microsoft.AspNetCore.Mvc;
using Restaurant_Tool_API.Services;

namespace Restaurant_Tool_API.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
[Produces("application/json")]
public class MenuController : ControllerBase
{
    private readonly IDataService _dataService;
    public MenuController(IDataService dataService)
    {
        _dataService = dataService;
    }

    /// <summary>
    /// Get menu.
    /// </summary>
    /// <response code="200">Returns menu.</response>
    /// <response code="404">If the menu is null.</response>
    [HttpGet]
    [ProducesResponseType(typeof(List<Models.Menu>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetMenu()
    {
        var result = _dataService.GetMenuListAsync();

        if (result == null) return this.NotFound("No Menu Items");

        return this.Ok(result);
    }
}
