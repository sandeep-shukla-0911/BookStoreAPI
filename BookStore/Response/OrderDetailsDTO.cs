using BookStore.Models;

namespace BookStore.Response
{
    public class OrderDetailsDTO
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public double? TotalAmount { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public string UserName { get; set; }
        public List<Books>? OrderItems { get; set; }
        public Address? deliveryAddress { get; set; }
        public OrderDetailsDTO()
        {
            OrderItems = [];
            deliveryAddress = new Address();
        }
    }
}
