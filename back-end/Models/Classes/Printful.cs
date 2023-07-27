using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Classes
{
    public class Printful
    {

    }

    public class PrintfulProductResponse
    {
        public int Code { get; set; }
        public string Status { get; set; }
        public PrintfulProduct[] Result { get; set; }
    }

    public class PrintfulProduct
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class PrintfulShippingRateResponse
    {
        public int Code { get; set; }
        public string Status { get; set; }
        public PrintfulShippingRate[] Result { get; set; }
    }

    public class PrintfulShippingRate
    {
        public string Service { get; set; }
        public decimal Rate { get; set; }
    }

    public class PrintfulOrderRequest
    {
        // Define properties for creating an order
    }

    public class PrintfulOrderResponse
    {
        public int Code { get; set; }
        public string Status { get; set; }
        public int Id { get; set; }
    }

    public class PrintfulOrderListResponse
    {
        public int Code { get; set; }
        public string Status { get; set; }
        public List<PrintfulOrderResponse> Result { get; set; }
    }
}
