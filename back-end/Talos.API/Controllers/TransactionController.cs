using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Stripe;
using System.Collections.Generic;
using Models.Classes;
using PayPalCheckoutSdk.Core;
using PayPalCheckoutSdk.Orders;

namespace Talos.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly IConfiguration _config;

        public TransactionController(IConfiguration config)
        {
            _config = config;
        }

        [HttpPost("Stripe")]
        public IActionResult CreatePaymentIntent([FromBody] PaymentIntentRequest request)
        {
            StripeConfiguration.ApiKey = _config.GetSection("Stripe")["ApiKey"];

            var options = new PaymentIntentCreateOptions
            {
                Amount = request.Amount,
                Currency = request.Currency,
                PaymentMethodTypes = new List<string>
                {
                    "card",
                },
            };

            var service = new PaymentIntentService();
            var intent = service.Create(options);

            return Ok(new PaymentIntentResponse { ClientSecret = intent.ClientSecret });
        }

        [HttpPost("Paypal")]
        public IActionResult CreateOrder([FromBody] Models.Classes.OrderRequest request)
        {
            var environment = new SandboxEnvironment(_config.GetSection("PayPal")["ClientId"], _config.GetSection("PayPal")["ClientSecret"]);
            var client = new PayPalHttpClient(environment);

            var itemList = new List<Item>();

            foreach (var item in request.Items)
            {
                itemList.Add(new Item()
                {
                    Name = item.Name,
                    Quantity = item.Quantity.ToString(),
                    Description = item.Description,
                    UnitAmount = new Money
                    {
                        CurrencyCode = request.Currency,
                        Value = item.Price.ToString("0.00")
                    },
                    Category = "PHYSICAL_GOODS"
                });
            }

            var order = new PayPalCheckoutSdk.Orders.OrderRequest()
            {
                CheckoutPaymentIntent = "CAPTURE",
                PurchaseUnits = new List<PurchaseUnitRequest>
                {
                    new PurchaseUnitRequest
                    {
                        AmountWithBreakdown = new AmountWithBreakdown
                        {
                            CurrencyCode = request.Currency,
                            Value = request.TotalAmount.ToString("0.00"),
                            AmountBreakdown = new AmountBreakdown
                            {
                                ItemTotal = new Money
                                {
                                    CurrencyCode = request.Currency,
                                    Value = request.TotalAmount.ToString("0.00")
                                }
                            }
                        },
                        Items = itemList
                    }
                },
                ApplicationContext = new ApplicationContext()
                {
                    BrandName = "Your Brand Name",
                    UserAction = "PAY_NOW",
                    ShippingPreference = "NO_SHIPPING"
                }
            };

            var orderCreateRequest = new OrdersCreateRequest();
            orderCreateRequest.Prefer("return=representation");
            orderCreateRequest.RequestBody(order);

            var orderResponse = client.Execute(orderCreateRequest).Result;

            return Ok("Orden pagada correctamente");
        }
    }
}
