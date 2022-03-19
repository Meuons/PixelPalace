
using System.ComponentModel.DataAnnotations;
namespace Project.Models
{
    public class Order
    {
        public int OrderID { get; set; }
        public string? Date { get; set; }
        public int? Sum { get; set; }
        public int? Amount { get; set; }
        public int? ProductID { get; set; }
        public int? AddressID { get; set; }

    }
}
