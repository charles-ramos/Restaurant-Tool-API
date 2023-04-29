using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
}
