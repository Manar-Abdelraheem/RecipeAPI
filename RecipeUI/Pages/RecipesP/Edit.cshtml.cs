using Microsoft.AspNetCore.Mvc;
using RecipeUI.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel;
using RecipeUI.Data;
using System.Text.Json;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace RecipeUI.Pages.RecipesP
{
        [BindProperties]
    public class EditModel : PageModel
    {
        public static HttpClient client = new HttpClient();
        public static Uri endPoint = new Uri("https://localhost:7208/api/Recipes");
        public Recipe? Recipe { get; set; }
        public async Task OnGetAsync(int? id)
        {
            var result = await client.GetAsync(endPoint);
            var options = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            using var json = await result.Content.ReadAsStreamAsync();
            var recipes = await JsonSerializer.DeserializeAsync<List<Recipe>>(json, options);
            Recipe = recipes.Find(x => x.Id == id);
        }
        public async Task<IActionResult> OnPostAsync(int id ) 
        {
            if (ModelState.IsValid)
            {
                var json = JsonSerializer.Serialize(Recipe);
                var payload = new StringContent(json, Encoding.UTF8, "application/json");
                await client.PutAsync($"https://localhost:7208/api/Recipes/{id}", payload);
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}
