using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Classes
{
    public class PayURequestModel
    {
        public string TransactionId { get; set; }
        public decimal Amount { get; set; }
        public string ProductInfo { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        // Add other necessary properties for your specific use case
    }

    public class PayUResponseModel
    {
        public string Status { get; set; }
        public string TransactionId { get; set; }
        public decimal Amount { get; set; }
        // Add other necessary properties to capture PayU response data
    }
}
