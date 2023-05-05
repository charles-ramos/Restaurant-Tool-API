using Microsoft.AspNetCore.Mvc;
using Restaurant_Tool_API.Services;

namespace Restaurant_Tool_API.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
[Produces("application/json")]
public class ReservationsController : ControllerBase
{
    private readonly IDataService _dataService;
    public ReservationsController(IDataService dataService)
    {
        _dataService = dataService;
    }

    /// <summary>
    /// Get all reservations.
    /// </summary>
    /// <response code="200">Returns reservations.</response>
    /// <response code="404">If the reservations are null.</response>
    [HttpGet]
    [ProducesResponseType(typeof(List<Models.Reservation>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetReservations()
    {
        var result = _dataService.GetReservationsAsync();

        if (result == null) return this.NotFound("No reservations");

        return this.Ok(result);
    }

    /// <summary>
    /// Add a new reservation.
    /// </summary>
    /// <response code="200">Returns the reservation.</response>
    /// <response code="400">If the reservation is null.</response>
    [HttpPut]
    [ProducesResponseType(typeof(Models.Reservation), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult AddReservation([FromBody] Models.Reservation reservation)
    {
        if (reservation == null) return this.BadRequest("reservation is null");

        var result = _dataService.AddReservationAsync(reservation);

        if (result == null) return this.BadRequest("Could not add reservation");

        return this.Ok(result);
    }

    /// <summary>
    /// Deletes a reservation by Id.
    /// </summary>
    /// <response code="200">If the reservation is successfully deleted.</response>
    /// <response code="400">If the ID is null.</response>
    [HttpPut("delete{id}")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult DeleteReservation(int id)
    {
        var result = _dataService.DeleteReservationByIdAsync(id);

        if (result == null || result.Result == false) return this.BadRequest("Could not delete reservation");

        return this.Ok(result);
    }
}
