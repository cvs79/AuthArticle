using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Auth0WebArticle.Controllers
{
    [Authorize]
    public class PingapiController : Controller
    {
        public async Task<IActionResult> Index()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;

            ////// Extract tokens
            string accessToken = claimsIdentity?.Claims.FirstOrDefault(c => c.Type == "access_token")?.Value;
            string idToken = claimsIdentity?.Claims.FirstOrDefault(c => c.Type == "id_token")?.Value;

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", idToken);
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "fill in");
            HttpResponseMessage response = await client.GetAsync("https://azureapimanagement.net/article/api/PingAPI");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            ViewBag.APIAnswer = responseBody;

            return View();
        }
    }
}