using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Models.Classes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace Talos.API.Controllers
{
    public class PrintifyController : Controller
    {
        private const string PrintifyApiBaseUrl = "https://api.printify.com/v1/";
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;
        public IConfiguration Configuration { get; }

        public PrintifyController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            Configuration = configuration;
            string apiKey = Configuration["PrintifyApi:ApiKey"];
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(PrintifyApiBaseUrl);
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // Set your Printify API key here
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
        }

        [HttpGet("products")]
        public async Task<IActionResult> GetAllProducts()
        {
            try
            {
                // Make a GET request to Printify's products endpoint
                var response = await _httpClient.GetAsync("products");

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var products = JsonConvert.DeserializeObject<List<PrintifyProduct>>(responseContent);
                return Ok(products);
                }
                else
                {
                    return BadRequest("Failed to get products from Printify.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("shipping-rates")]
        public async Task<IActionResult> GetShippingRates([FromBody] ShippingRateRequest shippingRateRequest)
        {
            try
            {
                // Make a POST request to Printify's shipping rates endpoint
                var response = await _httpClient.PostAsJsonAsync("shipping_rates", shippingRateRequest);

                if (response.IsSuccessStatusCode)
                {
                    // Deserialize the response into a model
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var shippingRates = JsonConvert.DeserializeObject<List<ShippingRateResponse>>(responseContent);
                    return Ok(shippingRates);
                }
                else
                {
                    return BadRequest("Failed to get shipping rates from Printify.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("orders")]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest orderRequest)
        {
            try
            {
                // Make a POST request to Printify's shipping rates endpoint
                var response = await _httpClient.PostAsJsonAsync("orders", orderRequest);

                if (response.IsSuccessStatusCode)
                {
                    // Deserialize the response into a model
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var order = JsonConvert.DeserializeObject<List<PrintifyOrder>>(responseContent);
                    return Ok(order);
                }
                else
                {
                    return BadRequest("Failed to get shipping rates from Printify.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // Endpoint to retrieve a single product by ID
        [HttpGet("products/{productId}")]
        public async Task<IActionResult> GetProductById(int productId)
        {
            try
            {
                // Make a POST request to Printify's shipping rates endpoint
                var response = await _httpClient.GetAsync($"products/{productId}");

                if (response.IsSuccessStatusCode)
                {
                    // Deserialize the response into a model
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var product = JsonConvert.DeserializeObject<List<PrintifyProduct>>(responseContent);
                    return Ok(product);
                }
                else
                {
                    return BadRequest("Failed to get shipping rates from Printify.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("order-status/{orderId}")]
        public async Task<IActionResult> GetOrderStatus(string orderId)
        {
            try
            {
                // Make a GET request to Printify's order status endpoint
                var response = await _httpClient.GetAsync($"orders/{orderId}/status");

                if (response.IsSuccessStatusCode)
                {
                    // Deserialize the response into a model
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var orderStatus = JsonConvert.DeserializeObject<List<OrderStatusResponse>>(responseContent);
                    return Ok(orderStatus);
                }
                else
                {
                    return BadRequest("Failed to get order status from Printify.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("orders")]
        public async Task<IActionResult> GetAllOrders()
        {
            try
            {
                // Make a GET request to Printify's orders endpoint
                var response = await _httpClient.GetAsync("orders");

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var orders = JsonConvert.DeserializeObject<List<PrintifyOrder>>(responseContent);
                    return Ok(orders);
                }
                else
                {
                    return BadRequest("Failed to get orders from Printify.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
