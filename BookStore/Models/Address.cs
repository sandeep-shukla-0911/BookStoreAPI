namespace BookStore.Models
{
    public class Address
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public string? AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }
        public string? City { get; set; }
        public string? Region { get; set; }
        public string? Country { get; set; }
        public string? Phone { get; set; }
        public string? Mobile { get; set; }
        public string? Email { get; set; }
        public string? ZipCode { get; set; }
    }
}
