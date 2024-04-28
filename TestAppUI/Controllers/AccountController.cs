using System.Net.Http;
using Microsoft.AspNetCore.Mvc;

namespace TestAppUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly HttpClient _httpClient;

        public AccountController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("http://localhost:5001");
        }

        public async Task<IActionResult> LogIn()
        {
            var response = await _httpClient.PostAsync("/Account/SignIn", null); 
            if (response.IsSuccessStatusCode)
            {
                var cookies = response.Headers.GetValues("Set-Cookie").ToList(); 
                foreach (var cookie in cookies)
                {
                    Response.Headers.Append("Set-Cookie", cookie);
                   
                }
                return RedirectToAction("Index", "Home"); 
            }
            else
            {
                return RedirectToAction("Index", "Home"); 
            }
        }



        public async Task<IActionResult> LogOut()
        {
            var response = await _httpClient.PostAsync("/Account/SignOut", null);
            if (response.IsSuccessStatusCode)
            {
                // W przypadku wylogowania poprawnie możemy przekierować użytkownika na dowolną stronę
                return RedirectToAction("Index", "Home");
            }
            else
            {
                // W przypadku błędu wylogowywania możemy obsłużyć to odpowiednio
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}