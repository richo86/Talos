using Microsoft.AspNetCore.Mvc;
using Models.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Talos.API.Controllers
{
    [Route("api/payu")]
    [ApiController]
    public class PayUController : Controller
    {
        private static readonly string MerchantKey = "";
        private static readonly string MerchantSalt = "";

        [HttpPost("initiate-payment")]
        public IActionResult InitiatePayment([FromBody] PayURequestModel requestModel)
        {
            // Validate the request and compute the hash
            if (!IsValidRequest(requestModel))
            {
                return BadRequest("Invalid request.");
            }

            // Calculate the hash based on your logic and PayU's requirements
            string hash = CalculateHash(requestModel);

            // Include the hash in your response and send it to the frontend
            return Ok(new { Hash = hash, PaymentURL = "https://secure.payu.in/_payment" });
        }

        [HttpPost("payment-success")]
        public async Task<IActionResult> PaymentSuccessCallback()
        {
            // Handle successful payment callback from PayU
            // This endpoint will receive a POST request from PayU with payment status

            // Parse and process the PayU response here

            return Ok("Payment successful.");
        }

        [HttpPost("payment-failure")]
        public async Task<IActionResult> PaymentFailureCallback()
        {
            // Handle failed payment callback from PayU
            // This endpoint will receive a POST request from PayU with payment status

            // Parse and process the PayU response here

            return Ok("Payment failed.");
        }

        // Helper methods for validation and hash calculation
        private bool IsValidRequest(PayURequestModel requestModel)
        {
            // Implement your validation logic here
            // You should validate request parameters according to your business rules
            return true; // Change this to your actual validation logic
        }

        private string CalculateHash(PayURequestModel requestModel)
        {
            // Implement your hash calculation logic here
            // Use MerchantKey, MerchantSalt, and requestModel data
            // Follow PayU's documentation for hash calculation

            // Example hash calculation:
            string data = $"{MerchantKey}|{requestModel.TransactionId}|{requestModel.Amount}|{MerchantSalt}";
            using (SHA512 shaM = new SHA512Managed())
            {
                byte[] hashBytes = shaM.ComputeHash(Encoding.UTF8.GetBytes(data));
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }
    }
}
