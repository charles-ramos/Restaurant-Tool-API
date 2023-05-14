using Microsoft.EntityFrameworkCore;
using Restaurant_Tool_API.Database;
using Restaurant_Tool_API.Models;
using Restaurant_Tool_API.Models.Enums;

namespace Restaurant_Tool_API.Services;

public class DataService : IDataService 
{
    private readonly DataContext _context;

    public DataService (DataContext context)    // Comments
    {
        _context = context;
    }

    // public methods

    // RESERVATION
    public async Task<IEnumerable<Models.Reservation>> GetReservationsAsync()
    {
        var reservations = await _context.Reservations.ToListAsync();   // get all reservations in database

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
        var result = await _context.Reservations.SingleOrDefaultAsync(x => x.Id == id); // get reservation with this Id in database

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
        var orders = await _context.Orders.ToListAsync();   // get all orders in database

        var result = ConvertOrderList(orders);  // convert orders from DB model to API/view model to return 

        return result;
    }

    public async Task<IEnumerable<Models.Order>> GetOrdersByTableIdAsync(int id)
    {
        var orders = await _context.Orders.Where(item => item.TableId == id).ToListAsync(); // get all orders with this table Id from database

        var result = ConvertOrderList(orders);  // convert orders from DB model to API/view model to return

        return result; 
    }

    public async Task<Models.Order> AddOrderAsync(Models.Order order)
    {
        var cOrder = ConvertOrder(order);  // convert order from API/view model to DB model
        var cOrderItems = ConvertOrderItemList(order.OrderItems, cOrder.Id); // convert order items from API/view model to DB model 

        await _context.AddAsync(cOrder);  // add order to database
        await _context.AddAsync(cOrderItems);  // add order items to database
        await _context.SaveChangesAsync();

        var result = ConvertOrder(cOrder, cOrderItems);    // convert order from DB model to API/view model to return

        return result;
    }


    // MENU 
    public async Task<IEnumerable<Models.Menu>> GetMenuListAsync()
    {
        var menuList = await _context.Menu.ToListAsync();  // get all menu items in database

        var result = ConvertMenuList(menuList); // convert menu from DB model to API/view model to return

        return result;
    }


    // BILL
    public async Task<Models.Bill> GetBillByOrdersAsync(List<Models.Order> orders, string paymentMethod)
    {
        var result = await CreateBillAsync(orders, paymentMethod);   // create bill 

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
            NumberOfPersons = reservation.NumberOfPersons,
            Date = reservation.Date,
            Time = reservation.Time,
            TableId = reservation.TableId,
            Name = reservation.Name
        };

        return result;
    }


    // ORDER
    private List<Models.Order> ConvertOrderList(List<Database.Order> orders)
    {
        var result = new List<Models.Order>();

        foreach (var order in orders)
        {
            var orderItems = GetOrderItemsByOrderIdAsync(order.Id).Result;
            var cOrder = ConvertOrder(order, orderItems);    // convert each item in list
            result.Add(cOrder);     // add converted item to return list
        }

        return result;
    }

    private Models.Order ConvertOrder(Database.Order order, List<Database.OrderItem> orderItems)     // convert order from DB model to API/view model
    {
        var result = new Models.Order
        {
            Id = order.Id,
            TableId = order.TableId,
            OrderItems = ConvertOrderItemList(orderItems)
        };

        return result;
    }

    private Database.Order ConvertOrder(Models.Order order)     // convert order from API/view model to DB model
    {
        var result = new Database.Order  
        {
            TableId = order.TableId
        };

        return result; 
    }

    private async Task<List<Models.Order>> GetOrdersByIdsAsync(string id)
    {
        var orderIds = id.Split(',').ToList();  // split string with IDs

        var orders = await _context.Orders.Where(item => orderIds.Contains(item.Id.ToString())).ToListAsync();  // get all orders with one of this IDs in database

        if (orders == null) return null;    // no orders with one of this IDs

        var result = ConvertOrderList(orders);  // convert orders from DB model to API/view model to return 

        return result;
    }


    // ORDER ITEMS
    private async Task<List<Database.OrderItem>> GetOrderItemsByOrderIdAsync(int id)
    {
        var result = await _context.OrderItems.Where(item => item.OrderId == id).ToListAsync();

        return result;
    }

    private List<Database.OrderItem> ConvertOrderItemList(List<Models.OrderItem> orderItems, int orderId)
    {
        var result = new List<Database.OrderItem>();

        foreach (var item in orderItems)
        {
            var orderItem = ConvertOrderItem(item, orderId);
            result.Add(orderItem);
        }

        return result;
    }

    private List<Models.OrderItem> ConvertOrderItemList(List<Database.OrderItem> orderItems)
    {
        var result = new List<Models.OrderItem>();

        foreach (var item in orderItems)
        {
            var orderItem = ConvertOrderItem(item);
            result.Add(orderItem);
        }

        return result;
    }

    private Database.OrderItem ConvertOrderItem(Models.OrderItem orderItem, int orderId)
    {
        var result = new Database.OrderItem
        {
            Note = orderItem.Note,
            OrderId = orderId,
            MenuId = orderItem.Product.Id
        };

        return result;
    }

    private Models.OrderItem ConvertOrderItem(Database.OrderItem orderItem)
    {
        var result = new Models.OrderItem
        {
            Note = orderItem.Note,
            Product = GetMenuItemByIdAsync(orderItem.MenuId).Result
        };

        return result;
    }


    // MENU 
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

    private async Task<Models.Menu> GetMenuItemByIdAsync(int id)
    {
        var item = _context.Menu.SingleOrDefault(item => item.Id == id);

        var result = ConvertMenuItem(item);
        
        return result;
    }


    // BILL
    private async Task<Models.Bill> CreateBillAsync(List<Models.Order> orders, string paymentMethod)
    {
        var totalPrice = orders.Select(order => order.OrderItems.Select(item => item.Product.Price).Sum()).Sum();
        var reservation = await _context.Reservations.SingleOrDefaultAsync(item => item.TableId == orders.First().TableId);
        var reservationId = reservation.Id;

        var bill = new Database.Bill   // create bill for database
        {
            Date = DateTime.Now,
            PaymentMethod = (int)Enum.Parse(typeof(PaymentMethod), paymentMethod),  // get enum integer value of this enum string name 
            Price = totalPrice,
            ReservationId = reservationId,
            OrderIds = string.Join(",", orders.Select(item => item.Id))  // concat all order IDs with comma 
        };

        await _context.AddAsync(bill);  // add and save bill in database
        await _context.SaveChangesAsync();

        var result = await ConvertBillAsync(bill);  // convert bill from DB model tp API/view model to return 
        
        return result;
    }

    private async Task<Models.Bill> ConvertBillAsync(Database.Bill bill)    // convert from DB model to API/view model
    {
        var orders = await GetOrdersByIdsAsync(bill.OrderIds);

        var result = new Models.Bill
        {
            Id = bill.Id,
            Date = bill.Date,
            PaymentMethod = Enum.GetName(typeof(PaymentMethod), bill.PaymentMethod),    // get enum string name of this enum integer value 
            Price = bill.Price,
            ReservationId = bill.ReservationId,
            Orders = orders
        };

        return result;
    }
}
