using System.Collections.Generic;

namespace Models.Classes
{
    public class OrderRequest
    {
        public List<ItemRequest> Items { get; set; }
        public string Currency { get; set; }
        public decimal TotalAmount { get; set; }
    }

    public class ItemRequest
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
    }

    public class OrderResponse
    {
        public string Id { get; set; }
    }
}
