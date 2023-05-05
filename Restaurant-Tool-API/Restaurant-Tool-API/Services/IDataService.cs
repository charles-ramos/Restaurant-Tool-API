using Restaurant_Tool_API.Models;
using System.Globalization;

namespace Restaurant_Tool_API.Services;

public interface IDataService
{
    Task<IEnumerable<Reservation>> GetReservationsAsync();

    Task<Reservation> AddReservationAsync(Reservation reservation);

    Task<bool> DeleteReservationByIdAsync(int id);

    Task<IEnumerable<Order>> GetOrdersAsync();

    Task<IEnumerable<Order>> GetOrdersByTableIdAsync(int id);

    Task<Order> AddOrderAsync(Order order);

    Task<IEnumerable<Menu>> GetMenuListAsync();

    Task<Bill> GetBillByReservationIdAsync(int reservationId, string paymentMethod);
}
