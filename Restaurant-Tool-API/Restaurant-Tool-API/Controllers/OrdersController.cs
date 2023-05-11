using Microsoft.AspNetCore.Mvc;
using Restaurant_Tool_API.Services;
using System.ComponentModel.DataAnnotations;

namespace Restaurant_Tool_API.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
[Produces("application/json")]
public class OrdersController : ControllerBase
{
    private readonly IDataService _dataService;
    public OrdersController(IDataService dataService)
    {
        _dataService = dataService;
    }

    /// <summary>
    /// Get all orders.
    /// </summary>
    /// <response code="200">Returns orders.</response>
    /// <response code="404">If orders are null.</response>
    [HttpGet]
    [ProducesResponseType(typeof(List<Models.Order>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetOrders()
    {
        var result = _dataService.GetOrdersAsync();

        if (result == null) return this.NotFound("No Orders");

        return this.Ok(result);
    }

    /// <summary>
    /// Get all orders by table Id.
    /// </summary>
    /// <response code="200">Returns the orders with this table Id.</response>
    /// <response code="404">If the ID is not found.</response>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(List<Models.Order>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetOrdersByTableId([Required] int id)
    {
        var result = _dataService.GetOrdersByTableIdAsync(id);

        if (result == null) return this.NotFound("No Orders with this ID");

        return this.Ok(result);
    }

    /// <summary>
    /// Add a new order.
    /// </summary>
    /// <response code="200">Returns the order.</response>
    /// <response code="400">If the order could not be added.</response>
    [HttpPut]
    [ProducesResponseType(typeof(Models.Order), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult AddOrder([FromBody, Required] Models.Order order)
    {
        var result = _dataService.AddOrderAsync(order);

        if (result == null) return this.BadRequest("Could not add order");

        return this.Ok(result);
    }
}
