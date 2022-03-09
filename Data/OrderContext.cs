using Microsoft.EntityFrameworkCore;
using Project.Models;

namespace Project.Data
{
    public class OrderContext: DbContext
    {

        public OrderContext(DbContextOptions<OrderContext> options) : base(options)

        {

        }

        public DbSet<Order> Orders { get; set; }
    }
}