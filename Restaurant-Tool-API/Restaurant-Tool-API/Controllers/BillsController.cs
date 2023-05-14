using Microsoft.AspNetCore.Mvc;
using Restaurant_Tool_API.Services;
using System.ComponentModel.DataAnnotations;

namespace Restaurant_Tool_API.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
[Produces("application/json")]
public class BillsController : ControllerBase
{
    private readonly IDataService _dataService;
    public BillsController(IDataService dataService)
    {
        _dataService = dataService;
    }

    /// <summary>
    /// Create a bill with this orders and payment method.
    /// </summary>
    /// <response code="200">Returns the bill.</response>
    /// <response code="400">If the bill is null.</response>
    [HttpPut]
    [ProducesResponseType(typeof(Models.Bill), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult CreateBill([FromBody, Required] List<Models.Order> orders, [Required] string paymentMethod)
    {
        var result = _dataService.GetBillByOrdersAsync(orders, paymentMethod);

        if (result == null) return this.BadRequest("Could not create bill");

        return this.Ok(result);
    }
}
