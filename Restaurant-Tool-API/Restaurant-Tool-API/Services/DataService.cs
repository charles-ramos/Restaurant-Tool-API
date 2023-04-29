using Microsoft.Extensions.Options;
using Restaurant_Tool_API.Database;

namespace Restaurant_Tool_API.Services;

public class DataService : IDataService
{
    private readonly DataContext _context;

    public DataService (DataContext context)
    {
        _context = context;
    }
}
