namespace Project.Models
{
    public class Address
    {
        public int AddressID { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
        public string? Street { get; set; }
        public string? Zipcode { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public List<Order>? Orders { get; set; }
    }
}
