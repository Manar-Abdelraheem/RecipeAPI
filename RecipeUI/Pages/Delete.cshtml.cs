using Microsoft.AspNetCore.Mvc;
using RecipeUI.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel;
using System.Text.Json;
using System.Text;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace RecipeUI.Pages
{
        [BindProperties]
    public class DeleteModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public DeleteModel(IHttpClientFactory httpClientFactory) => _httpClientFactory = httpClientFactory;
        public Recipe? Recipe { get; set; } = default!;
        public async Task OnGetAsync(int? id)
        {
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, "https://localhost:7208/api/Recipes");
            var options = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            var httpClient = _httpClientFactory.CreateClient();
            var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);
            using var json = await httpResponseMessage.Content.ReadAsStreamAsync();
            var recipes = await JsonSerializer.DeserializeAsync<List<Recipe>?>(json, options);
            if (recipes == null)
            {
                Page();
            }
                Recipe = recipes!.Find(x => x.Id == id);
        }
        public async Task<IActionResult> OnPostAsync(int id ) 
        {
                var httpClient = _httpClientFactory.CreateClient();
                if (Recipe != null)
                { 
                    Recipe.Id = id;
                    using var httpResponseMessage = await httpClient.DeleteAsync($"https://localhost:7208/api/Recipes/{id}");
                    httpResponseMessage.EnsureSuccessStatusCode();
                    return RedirectToPage("Index");
                }
            return Page();
        }
    }
}
