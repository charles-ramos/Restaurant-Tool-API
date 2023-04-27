using Microsoft.EntityFrameworkCore;

namespace Restaurant_Tool_API.Database;

public class DataContext : DbContext
{
    public DataContext (DbContextOptions<DataContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Tables> TableItems { get; set; }
    public virtual DbSet<Reservations> ReservationItems { get; set; }
    public virtual DbSet<Orders> OrderItems { get; set; }
    public virtual DbSet<Menu> MenuItems { get; set; }
    public virtual DbSet<Bills> BillItems { get; set; }
}
