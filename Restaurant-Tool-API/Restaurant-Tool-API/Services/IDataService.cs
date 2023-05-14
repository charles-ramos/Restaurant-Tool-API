using Restaurant_Tool_API.Models;
using System.Globalization;

namespace Restaurant_Tool_API.Services;

public interface IDataService
{
    /// <summary>
    /// Get all reservations.
    /// </summary>
    /// <returns>An enumerable with all reservations.</returns>
    Task<IEnumerable<Reservation>> GetReservationsAsync();

    /// <summary>
    /// Add a reservation to the database.
    /// </summary>
    /// <param name="reservation">A reservation model.</param>
    /// <returns>The reservation model which was added to the database.</returns>
    Task<Reservation> AddReservationAsync(Reservation reservation);

    /// <summary>
    /// Delete a reservation with this Id.
    /// </summary>
    /// <param name="id">A unique int Id for this reservation.</param>
    /// <returns>A boolean whether the reservation was successfully deleted.</returns>
    Task<bool> DeleteReservationByIdAsync(int id);

    /// <summary>
    /// Get all orders.
    /// </summary>
    /// <returns>An enumerable with all existing orders.</returns>
    Task<IEnumerable<Order>> GetOrdersAsync();

    /// <summary>
    /// Get all orders for this table.
    /// </summary>
    /// <param name="id">A unique int Id for a table.</param>
    /// <returns>An enumerable with all orders for this table.</returns>
    Task<IEnumerable<Order>> GetOrdersByTableIdAsync(int id);

    /// <summary>
    /// Add a new order to the database.
    /// </summary>
    /// <param name="order">An order model.</param>
    /// <returns>The order model which was added to the database.</returns>
    Task<Order> AddOrderAsync(Order order);

    /// <summary>
    /// Get all menu items.
    /// </summary>
    /// <returns>An enumerable with all menu items.</returns>
    Task<IEnumerable<Menu>> GetMenuListAsync();

    /// <summary>
    /// Get a bill.
    /// </summary>
    /// <param name="orders">A list of orders which has to be added to the bill.</param>
    /// <param name="paymentMethod">A string with the payment method.</param>
    /// <returns>A bill model which was created.</returns>
    Task<Bill> GetBillByOrdersAsync(List<Order> orders, string paymentMethod);
}
