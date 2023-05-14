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

    public virtual DbSet<Table> Tables { get; set; }
    public virtual DbSet<Reservation> Reservations { get; set; }
    public virtual DbSet<Order> Orders { get; set; }
    public virtual DbSet<OrderItem> OrderItems { get; set; }
    public virtual DbSet<Menu> Menu { get; set; }
    public virtual DbSet<Bill> Bills { get; set; }
}
