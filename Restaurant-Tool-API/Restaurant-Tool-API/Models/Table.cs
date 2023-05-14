using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurant_Tool_API.Models;

public class Table
{
    public int Id { get; set; }

    public int NumberOfSeats { get; set; }
}
