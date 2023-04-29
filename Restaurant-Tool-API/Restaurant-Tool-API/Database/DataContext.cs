using Microsoft.EntityFrameworkCore;

namespace Restaurant_Tool_API.Database;

public class DataContext : DbContext
{
    protected readonly IConfiguration Configuration;
    public DataContext (DbContextOptions<DataContext> options, IConfiguration configuration) : base(options)
    {
        Configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(Configuration.GetConnectionString("DefaultConnection"));
    }

    public virtual DbSet<Tables> TableItems { get; set; }
    public virtual DbSet<Reservations> ReservationItems { get; set; }
    public virtual DbSet<Orders> OrderItems { get; set; }
    public virtual DbSet<Menu> MenuItems { get; set; }
    public virtual DbSet<Bills> BillItems { get; set; }
}
