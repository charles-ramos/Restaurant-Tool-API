using Microsoft.EntityFrameworkCore;
using Restaurant_Tool_API.Database;
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

    // TABLES
    public async Task<IEnumerable<Models.Tables>> GetTablesAsync()
    {
        var tables = await _context.TableItems.ToListAsync();   // get all tables in database

        var result = ConvertTableList(tables);  // convert tables from DB model to API/view model to return

        return result;
    }


    // RESERVATION
    public async Task<IEnumerable<Models.Reservations>> GetReservationsAsync()
    {
        var reservations = await _context.ReservationItems.ToListAsync();   // get all reservations in database

        var result = ConvertReservationList(reservations);  // convert reservations from DB model to API/view model to return

        return result;
    }

    public async Task<Models.Reservations> AddReservationAsync(Models.Reservations reservation)
    {
        var item = new Database.Reservations    // convert reservation from API/view model to DB model
        {
            Id = reservation.Id,
            Count = reservation.Count,
            Date = reservation.Date,
            Time = reservation.Time,
            TableId = reservation.Table.Id
        };
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
    public async Task<IEnumerable<Models.Orders>> GetOrdersAsync()
    {
        var orders = await _context.OrderItems.ToListAsync();   // get all orders in database

        var result = ConvertOrderList(orders);  // convert orders from DB model to API/view model to return 

        return result;
    }

    public async Task<IEnumerable<Models.Orders>> GetOrdersByTableIdAsync(int id)
    {
        var orders = await _context.OrderItems.Where(item => item.TableId == id).ToListAsync(); // get all orders with this table Id from database

        var result = ConvertOrderList(orders);  // convert orders from DB model to API/view model to return

        return result; 
    }

    public async Task<Models.Orders> AddOrderAsync(Models.Orders order)
    {
        var item = new Database.Orders  // convert order from API/view model to DB model
        {
            Id = order.Id,
            MenuIds = string.Join(",", order.MenuList.Select(item => item.Id)), // concat all menu item IDs with comma
            ReservationId = order.Reservation.Id,
            TableId = order.Table.Id
        };

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
    public async Task<Models.Bills> GetBillByReservationIdAsync(int reservationId, string paymentMethod)
    {
        var orders = await _context.OrderItems.Where(item => item.ReservationId == reservationId).ToListAsync();    // get all orders with this reservation Id in database

        if (!orders.Any()) return null; // no orders with this reservation Id

        var result = await CreateBillAsync(orders, reservationId, paymentMethod);   // create bill 

        return result;
    }
    

    // private methods

    // TABLES
    private List<Models.Tables> ConvertTableList(List<Database.Tables> tables)  
    {
        var result = new List<Models.Tables>();

        foreach (var table in tables)
        {
            var resultItem = ConvertTable(table);   // convert each item in list 
            result.Add(resultItem);     // add converted item to return list 
        }

        return result;
    }

    private Models.Tables ConvertTable(Database.Tables table)
    {
        var result = new Models.Tables
        {
            Id = table.Id,
            Seats = table.Seats
        };

        return result;
    }

    private async Task<Models.Tables> GetTableByIdAsync(int id)
    {
        var table = await _context.TableItems.SingleOrDefaultAsync(item => item.Id == id);  // get table with this Id in database

        if (table == null) return null; // no table with this Id

        var result = ConvertTable(table);   // convert table from DB model to API/view model 

        return result;
    }


    // RESERVATION
    private List<Models.Reservations> ConvertReservationList(List<Database.Reservations> reservations)  
    {
        var result = new List<Models.Reservations>();

        foreach (var reservation in reservations)
        {
            var reservationItem = ConvertReservation(reservation);  // convert each item in list
            result.Add(reservationItem);    // add converted item to return list
        }

        return result;
    }

    private Models.Reservations ConvertReservation(Database.Reservations reservation)
    {
        var result = new Models.Reservations
        {
            Id = reservation.Id,
            Count = reservation.Count,
            Date = reservation.Date,
            Time = reservation.Time,
            Table = GetTableByIdAsync(reservation.TableId).Result
        };

        return result;
    }

    private async Task<Models.Reservations> GetReservationByIdAsync(int id)
    {
        var reservation = await _context.ReservationItems.SingleOrDefaultAsync(item => item.Id == id);  // get reservation with this Id in database

        if (reservation == null) return null;   // no reservation with this Id

        var result = ConvertReservation(reservation);   // convert reservation from DB model to API/view model to return

        return result;
    }

    
    // ORDER
    private List<Models.Orders> ConvertOrderList(List<Database.Orders> orders)
    {
        var result = new List<Models.Orders>();

        foreach (var order in orders)
        {
            var orderItem = ConvertOrder(order);    // convert each item in list
            result.Add(orderItem);     // add converted item to return list
        }

        return result;
    }

    private Models.Orders ConvertOrder(Database.Orders order)
    {
        var result = new Models.Orders
        {
            Id = order.Id,
            Table = GetTableByIdAsync(order.TableId).Result,
            Reservation = GetReservationByIdAsync(order.ReservationId).Result,
            MenuList = GetMenuItemsByIdsAsync(order.MenuIds).Result
        };

        return result;
    }

    private async Task<List<Models.Orders>> GetOrdersByIdsAsync(string id)
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

    private Models.Menu ConvertMenuItem(Database.Menu menu)
    {
        var result = new Models.Menu
        {
            Id = menu.Id,
            Title = menu.Title,
            Description = menu.Description,
            Price = menu.Price.ToString() + " EUR",
            Category = Enum.GetName(typeof(MenuCategory), menu.Category)    // get name of enum with this integer
        };

        return result;
    }


    // BILL
    private async Task<Models.Bills> CreateBillAsync(List<Database.Orders> orders, int reservationId, string paymentMethod)
    {
        var menuList = await GetMenuItemsByIdsAsync(string.Join(",", orders.Select(item => item.MenuIds))); // concat menu item IDs in orders with comma; get menu items by ID string 
        var totalPrice = menuList.Sum(item => double.Parse(item.Price));    // sum price of all ordered menu items 

        var bill = new Database.Bills   // create bill for database
        {
            Date = DateTime.Now,
            PaymentMethod = (int)Enum.Parse(typeof(PaymentMethod), paymentMethod),  // get enum integer value of this enum string name 
            ReservationId = reservationId,
            TotalPrice = totalPrice,
            OrderIds = string.Join(",", orders.Select(item => item.Id)) // concat all order IDs with comma 
        };

        await _context.AddAsync(bill);  // add and save bill in database
        await _context.SaveChangesAsync();

        var result = await ConvertBillAsync(bill);  // convert bill from DB model tp API/view model to return 
        
        return result;
    }

    private async Task<Models.Bills> ConvertBillAsync(Database.Bills bill)
    {
        var orderList = await GetOrdersByIdsAsync(bill.OrderIds);
        var result = new Models.Bills
        {
            Id = bill.Id,
            Date = bill.Date,
            PaymentMethod = Enum.GetName(typeof(PaymentMethod), bill.PaymentMethod),    // get enum string name of this enum integer value 
            TotalPrice = bill.TotalPrice.ToString() + " EUR",
            ReservationId = bill.ReservationId,
            Orders = orderList
        };

        return result;
    }
}
