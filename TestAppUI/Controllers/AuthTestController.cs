using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;

namespace TestAppUI.Controllers
{
    public class AuthTestController : Controller
    {
        private readonly HttpClient _httpClient;

        public AuthTestController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("http://localhost:5001"); // Adres API
        }

        public async Task<IActionResult> AuthOnlyAction()
        {
            // Wywołanie akcji API z autoryzacją
            var response = await _httpClient.GetAsync("/AuthTest/AuthOnlyAction");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Content(content);
            }
            else
            {
                return StatusCode((int)response.StatusCode);
            }
        }
        
        public async Task<IActionResult> AuthTest()
        {
            // Wywołanie akcji API do testowania autoryzacji
            var response = await _httpClient.GetAsync("/AuthTest/AuthTest");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Content(content);
            }
            else
            {
                return StatusCode((int)response.StatusCode);
            }
        }
    }
}