using AutoMapper;
using Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Models.Classes;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using Talos.API.User;

namespace Talos.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrintController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;
        public IConfiguration Configuration { get; }

        public PrintController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            Configuration = configuration;
            string apiKey = Configuration["PrintfulApi:ApiKey"];
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new System.Uri("https://api.printful.com/");
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        [HttpGet("products")]
        public async Task<IActionResult> GetProducts()
        {
            var response = await _httpClient.GetAsync("products");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var products = JsonSerializer.Deserialize<PrintfulProductResponse>(json, _jsonOptions);
                return Ok(products);
            }

            return StatusCode((int)response.StatusCode);
        }

        [HttpGet("shipping-rates")]
        public async Task<IActionResult> GetShippingRates(int countryId, string stateCode = null, string zip = null)
        {
            var url = $"shipping/rates?country={countryId}";
            if (!string.IsNullOrEmpty(stateCode))
                url += $"&state={stateCode}";
            if (!string.IsNullOrEmpty(zip))
                url += $"&zip={zip}";

            var response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var rates = JsonSerializer.Deserialize<PrintfulShippingRateResponse>(json, _jsonOptions);
                return Ok(rates);
            }

            return StatusCode((int)response.StatusCode);
        }

        [HttpPost("create-order")]
        public async Task<IActionResult> CreateOrder([FromBody] PrintfulOrderRequest orderRequest)
        {
            var json = JsonSerializer.Serialize(orderRequest, _jsonOptions);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("orders", content);
            if (response.IsSuccessStatusCode)
            {
                var orderJson = await response.Content.ReadAsStringAsync();
                var order = JsonSerializer.Deserialize<PrintfulOrderResponse>(orderJson, _jsonOptions);
                return Ok(order);
            }

            return StatusCode((int)response.StatusCode);
        }

        [HttpGet("orders/{orderId}")]
        public async Task<IActionResult> GetOrder(int orderId)
        {
            var response = await _httpClient.GetAsync($"orders/{orderId}");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var order = JsonSerializer.Deserialize<PrintfulOrderResponse>(json, _jsonOptions);
                return Ok(order);
            }

            return StatusCode((int)response.StatusCode);
        }

        [HttpGet("getOrders")]
        public async Task<IActionResult> GetAllOrders()
        {
            var response = await _httpClient.GetAsync("orders");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var orders = JsonSerializer.Deserialize<PrintfulOrderListResponse>(json, _jsonOptions);
                return Ok(orders);
            }

            return StatusCode((int)response.StatusCode);
        }
    }
}
