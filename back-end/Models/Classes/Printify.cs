using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Classes
{
    public class PrintifyProduct
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        // Add other properties as needed
    }

    public class ShippingRateRequest
    {
        public string DestinationCountryCode { get; set; }
        public string DestinationState { get; set; }
        public string DestinationCity { get; set; }
        public string DestinationZipCode { get; set; }
        public string ProductId { get; set; }
        public int Quantity { get; set; }
        public string ShippingMethod { get; set; }
        // Add other properties as needed
    }

    public class ShippingRateResponse
    {
        public decimal ShippingFee { get; set; }
        // Add other properties as needed
    }

    public class PrintifyOrder
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public decimal TotalPrice { get; set; }
        // Add other order-related properties as needed
    }

    public class CreateOrderRequest
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public string ShippingAddress { get; set; }
        // Add other order-related properties as needed
    }

    public class OrderStatusResponse
    {
        public string Status { get; set; }
        // Add other properties as needed
    }
}
