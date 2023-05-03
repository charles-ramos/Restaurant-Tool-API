using Restaurant_Tool_API.Models;
using System.Globalization;

namespace Restaurant_Tool_API.Services;

public interface IDataService
{
    Task<IEnumerable<Tables>> GetTablesAsync();

    Task<IEnumerable<Reservations>> GetReservationsAsync();

    Task<Reservations> AddReservationAsync(Reservations reservation);

    Task<bool> DeleteReservationByIdAsync(int id);

    Task<IEnumerable<Orders>> GetOrdersAsync();

    Task<IEnumerable<Orders>> GetOrdersByTableIdAsync(int id);

    Task<Orders> AddOrderAsync(Orders order);

    Task<IEnumerable<Menu>> GetMenuListAsync();

    Task<Bills> GetBillByReservationIdAsync(int reservationId, string paymentMethod);
}
