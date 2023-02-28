using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RecipeUI.Models;
using System.Text.Json;

namespace RecipeUI.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public IndexModel(IHttpClientFactory httpClientFactory) => _httpClientFactory = httpClientFactory;
        public IEnumerable<Recipe>? Recipes { get; set; }
        public async Task OnGet()
        {
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get,"https://localhost:7208/api/Recipes");
            var options = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            if(ModelState.IsValid)
            {
                var httpClient = _httpClientFactory.CreateClient();
                var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);
                using var json = await httpResponseMessage.Content.ReadAsStreamAsync();
                Recipes = await JsonSerializer.DeserializeAsync<List<Recipe>>(json, options);
            }
        }
    }
}