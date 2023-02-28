using Microsoft.AspNetCore.Mvc;
using RecipeUI.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel;
//using RecipeUI.Data;
using System.Text.Json;
using System.Text;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace RecipeUI.Pages.RecipesP
{
        [BindProperties]
    public class EditModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public EditModel(IHttpClientFactory httpClientFactory) => _httpClientFactory = httpClientFactory;
        public Recipe? Recipe { get; set; }
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
            var recipes = await JsonSerializer.DeserializeAsync<List<Recipe>>(json, options);
            if (recipes != null)
            {
                Recipe = recipes.Find(x => x.Id == id);
            }
        }
        public async Task<IActionResult> OnPostAsync(int id ) 
        {
            if (ModelState.IsValid)
            {
                var httpClient = _httpClientFactory.CreateClient();
                var json = new StringContent(JsonSerializer.Serialize(Recipe), Encoding.UTF8, Application.Json);
                using var httpResponseMessage = await httpClient.PutAsync($"https://localhost:7208/api/Recipes/{id}", json);
                httpResponseMessage.EnsureSuccessStatusCode();
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}
