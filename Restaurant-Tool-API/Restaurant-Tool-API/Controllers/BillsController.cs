using Microsoft.AspNetCore.Mvc;
using Restaurant_Tool_API.Services;

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
    /// Create a bill by reservation Id and payment method.
    /// </summary>
    /// <response code="200">Returns the bill for this reservation Id.</response>
    /// <response code="400">If the bill is null.</response>
    [HttpPut]
    [ProducesResponseType(typeof(Models.Bill), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult CreateBill(int reservationId, string paymentMethod)
    {
        if (paymentMethod == null) return this.BadRequest("no payment method");

        var result = _dataService.GetBillByReservationIdAsync(reservationId, paymentMethod);

        if (result == null) return this.BadRequest("Could not create bill");

        return this.Ok(result);
    }
}
