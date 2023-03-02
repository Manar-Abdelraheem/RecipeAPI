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
        public IEnumerable<Recipe> Recipes { get; set; } = default!;
        public async Task OnGet()
        {
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, "https://localhost:7208/api/Recipes");
            var options = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
                var httpClient = _httpClientFactory.CreateClient();
                var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);
                using var json = await httpResponseMessage.Content.ReadAsStreamAsync();
            if ( json == null)
            {
                BadRequest();
            }
                Recipes = await JsonSerializer.DeserializeAsync<List<Recipe>>(json!, options!);
        }
    }
}