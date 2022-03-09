using Microsoft.EntityFrameworkCore;
using Project.Models;

namespace Project.Data
{
    public class AddressContext : DbContext
    {

        public AddressContext(DbContextOptions<AddressContext> options) : base(options)

        {

        }

        public DbSet<Address> Addresses { get; set; }
    }
}