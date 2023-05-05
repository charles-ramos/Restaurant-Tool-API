using Microsoft.EntityFrameworkCore;

namespace Restaurant_Tool_API.Database;

public class DataContext : DbContext
{
    protected readonly IConfiguration _configuration;
    public DataContext (DbContextOptions<DataContext> options, IConfiguration configuration) : base(options)
    {
        _configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(_configuration.GetConnectionString("DefaultConnection"));
    }

    public virtual DbSet<Table> TableItems { get; set; }
    public virtual DbSet<Reservation> ReservationItems { get; set; }
    public virtual DbSet<Order> OrderItems { get; set; }
    public virtual DbSet<Menu> MenuItems { get; set; }
    public virtual DbSet<Bill> BillItems { get; set; }
}
