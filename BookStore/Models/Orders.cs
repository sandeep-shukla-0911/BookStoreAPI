namespace BookStore.Models
{
    public class Orders
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int AddressId { get; set; }
        public double? TotalAmount { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? DeliveryDate => OrderDate?.AddDays(7);
    }
}
