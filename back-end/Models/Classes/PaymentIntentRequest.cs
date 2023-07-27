using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Classes
{
    public class PaymentIntentRequest
    {
        public int Amount { get; set; }
        public string Currency { get; set; }
    }
}
