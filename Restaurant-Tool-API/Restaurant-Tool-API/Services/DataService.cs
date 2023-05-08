using Microsoft.EntityFrameworkCore;
using Restaurant_Tool_API.Database;
using Restaurant_Tool_API.Models;
using Restaurant_Tool_API.Models.Enums;

namespace Restaurant_Tool_API.Services;

public class DataService : IDataService 
{
    private readonly DataContext _context;

    public DataService (DataContext context)
    {
        _context = context;
    }

    // public methods

    // RESERVATION
    public async Task<IEnumerable<Models.Reservation>> GetReservationsAsync()
    {
        var reservations = await _context.ReservationItems.ToListAsync();   // get all reservations in database

        var result = ConvertReservationList(reservations);  // convert reservations from DB model to API/view model to return

        return result;
    }

    public async Task<Models.Reservation> AddReservationAsync(Models.Reservation reservation)
    {
        var item = ConvertReservation(reservation);  // convert reservation from API/view model to DB model
  
        await _context.AddAsync(item);      // add and save DB model in database
        await _context.SaveChangesAsync();

        var result = ConvertReservation(item);  // convert reservation from DB model to API/view model to return 

        return result;
    }

    public async Task<bool> DeleteReservationByIdAsync(int id)
    {
        var result = await _context.ReservationItems.SingleOrDefaultAsync(x => x.Id == id); // get reservation with this Id in database

        if (result != null)     // if a reservation with this Id exists, delete the reservation 
        {
            _context.Remove(result);
            await _context.SaveChangesAsync();
            return true;
        }

        return false;
    }


    // ORDER
    public async Task<IEnumerable<Models.Order>> GetOrdersAsync()
    {
        var orders = await _context.OrderItems.ToListAsync();   // get all orders in database

        var result = ConvertOrderList(orders);  // convert orders from DB model to API/view model to return 

        return result;
    }

    public async Task<IEnumerable<Models.Order>> GetOrdersByTableIdAsync(int id)
    {
        var orders = await _context.OrderItems.Where(item => item.TableId == id).ToListAsync(); // get all orders with this table Id from database

        var result = ConvertOrderList(orders);  // convert orders from DB model to API/view model to return

        return result; 
    }

    public async Task<Models.Order> AddOrderAsync(Models.Order order)
    {
        var item = ConvertOrder(order);  // convert order from API/view model to DB model

        await _context.AddAsync(item);  // add and save order in database
        await _context.SaveChangesAsync();

        var result = ConvertOrder(item);    // convert order from DB model to API/view model to return

        return result;
    }


    // MENU 
    public async Task<IEnumerable<Models.Menu>> GetMenuListAsync()
    {
        var menuList = await _context.MenuItems.ToListAsync();  // get all menu items in database

        var result = ConvertMenuList(menuList); // convert menu from DB model to API/view model to return

        return result;
    }


    // BILL
    public async Task<Models.Bill> GetBillByReservationIdAsync(int reservationId, string paymentMethod)
    {
        var orders = await _context.OrderItems.Where(item => item.ReservationId == reservationId).ToListAsync();    // get all orders with this reservation Id in database

        if (!orders.Any()) return null; // no orders with this reservation Id

        var result = await CreateBillAsync(orders, reservationId, paymentMethod);   // create bill 

        return result;
    }
    

    // private methods

    // RESERVATION
    private List<Models.Reservation> ConvertReservationList(List<Database.Reservation> reservations)  
    {
        var result = new List<Models.Reservation>();

        foreach (var reservation in reservations)
        {
            var reservationItem = ConvertReservation(reservation);  // convert each item in list
            result.Add(reservationItem);    // add converted item to return list
        }

        return result;
    }

    private Models.Reservation ConvertReservation(Database.Reservation reservation) // convert from DB model to API/view model
    {
        var result = new Models.Reservation
        {
            Id = reservation.Id,
            Name = reservation.Name, 
            NumberOfPersons = reservation.NumberOfPersons,
            Date = reservation.Date,
            Time = reservation.Time,
            TableId = reservation.TableId
        };

        return result;
    }

    private Database.Reservation ConvertReservation(Models.Reservation reservation) // convert from API/view model to DB model
    {
        var result = new Database.Reservation    
        {
            Id = reservation.Id,
            NumberOfPersons = reservation.NumberOfPersons,
            Date = reservation.Date,
            Time = reservation.Time,
            TableId = reservation.TableId
        };

        return result;
    }

    
    // ORDER
    private List<Models.Order> ConvertOrderList(List<Database.Order> orders)
    {
        var result = new List<Models.Order>();

        foreach (var order in orders)
        {
            var orderItem = ConvertOrder(order);    // convert each item in list
            result.Add(orderItem);     // add converted item to return list
        }

        return result;
    }

    private Models.Order ConvertOrder(Database.Order order)     // convert order from DB model to API/view model
    {
        var result = new Models.Order
        {
            Id = order.Id,
            TableId = order.TableId,
            ReservationId = order.ReservationId,
            MenuList = GetMenuItemsByIdsAsync(order.MenuIds).Result
        };

        return result;
    }

    private Database.Order ConvertOrder(Models.Order order)     // convert order from API/view model to DB model
    {
        var result = new Database.Order  
        {
            Id = order.Id,
            MenuIds = string.Join(",", order.MenuList.Select(item => item.Id)), // concat all menu item IDs with comma
            ReservationId = order.ReservationId,
            TableId = order.TableId,
            Note = order.Note
        };

        return result; 
    }

    private async Task<List<Models.Order>> GetOrdersByIdsAsync(string id)
    {
        var orderIds = id.Split(',').ToList();  // split string with IDs

        var orders = await _context.OrderItems.Where(item => orderIds.Contains(item.Id.ToString())).ToListAsync();  // get all orders with one of this IDs in database

        if (orders == null) return null;    // no orders with one of this IDs

        var result = ConvertOrderList(orders);  // convert orders from DB model to API/view model to return 

        return result;
    }


    // MENU
    private async Task<List<Models.Menu>> GetMenuItemsByIdsAsync(string id)
    {
        var menuIds = id.Split(',').ToList();   // split string with IDs 

        var menuItems = await _context.MenuItems.Where(item => menuIds.Contains(item.Id.ToString())).ToListAsync(); // get all menu items with one of this IDs in database

        if (menuItems == null) return null; // no menu items with one of this IDs

        var result = ConvertMenuList(menuItems);    // convert menu from DB model to API/view model to return 

        return result;
    }

    private List<Models.Menu> ConvertMenuList(List<Database.Menu> menuItems)
    {
        var result = new List<Models.Menu>();

        foreach (var item in menuItems)
        {
            var menuItem = ConvertMenuItem(item);   // convert each item in list 
            result.Add(menuItem);   // add converted item to return list
        }

        return result;
    }

    private Models.Menu ConvertMenuItem(Database.Menu menu) // convert from DB model to API/view model
    {
        var result = new Models.Menu
        {
            Id = menu.Id,
            Name = menu.Name,
            Ingredients = menu.Ingredients,
            Price = menu.Price,
            Category = Enum.GetName(typeof(MenuCategory), menu.Category),     // get name of enum with this integer
        };

        return result;
    }


    // BILL
    private async Task<Models.Bill> CreateBillAsync(List<Database.Order> orders, int reservationId, string paymentMethod)
    {
        var menuList = await GetMenuItemsByIdsAsync(string.Join(",", orders.Select(item => item.MenuIds))); // concat menu item IDs in orders with comma; get menu items by ID string 
        var totalPrice = menuList.Sum(item => item.Price);    // sum price of all ordered menu items 

        var bill = new Database.Bill   // create bill for database
        {
            Date = DateTime.Now,
            PaymentMethod = (int)Enum.Parse(typeof(PaymentMethod), paymentMethod),  // get enum integer value of this enum string name 
            ReservationId = reservationId,
            TotalPrice = totalPrice,
            OrderIds = string.Join(",", orders.Select(item => item.Id))  // concat all order IDs with comma 
        };

        await _context.AddAsync(bill);  // add and save bill in database
        await _context.SaveChangesAsync();

        var result = await ConvertBillAsync(bill);  // convert bill from DB model tp API/view model to return 
        
        return result;
    }

    private async Task<Models.Bill> ConvertBillAsync(Database.Bill bill)    // convert from DB model to API/view model
    {
        var orderList = await GetOrdersByIdsAsync(bill.OrderIds);
        var result = new Models.Bill
        {
            Id = bill.Id,
            Date = bill.Date,
            PaymentMethod = Enum.GetName(typeof(PaymentMethod), bill.PaymentMethod),    // get enum string name of this enum integer value 
            TotalPrice = bill.TotalPrice,
            ReservationId = bill.ReservationId,
            Orders = orderList
        };

        return result;
    }
}
