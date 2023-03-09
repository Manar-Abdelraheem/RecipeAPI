using Microsoft.AspNetCore.Mvc;
using RecipeUI.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel;
using System.Text.Json;
using System.Text;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.IdentityModel.Tokens;

namespace RecipeUI.Pages
{
        [BindProperties]
    public class EditModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public EditModel(IHttpClientFactory httpClientFactory) => _httpClientFactory = httpClientFactory;
        public Recipe? Recipe { get; set; } = default!;
        public async Task OnGetAsync(int? id)
        {
            if (Recipe == null)
            {
                BadRequest();
            }
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, "https://localhost:7208/api/Recipes");
            var options = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            var httpClient = _httpClientFactory.CreateClient();
            var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);
            using var json = await httpResponseMessage.Content.ReadAsStreamAsync();
            var recipes = await JsonSerializer.DeserializeAsync<List<Recipe>>(json, options);
            if (recipes == null)
            {
                Page();
            }
                Recipe = recipes!.Find(x => x.Id == id);
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
