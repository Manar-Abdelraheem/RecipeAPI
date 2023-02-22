using Microsoft.AspNetCore.Mvc;
using RecipeUI.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel;
using RecipeUI.Data;
using System.Text.Json;
using System.Text;

namespace RecipeUI.Pages.RecipesP
{
        [BindProperties]
    public class CreateModel : PageModel
    {
        public static HttpClient client = new HttpClient();
        public static Uri endPoint = new Uri("https://localhost:7208/api/Recipes");
        public void OnGet()
        {

        }
        public Recipe Recipe { get; set; }
        public async Task<IActionResult> OnPost(Recipe recipe) 
        {
            if (ModelState.IsValid)
            {
                var json = JsonSerializer.Serialize(recipe);
                var payload = new StringContent(json, Encoding.UTF8, "application/json");
                await client.PostAsync(endPoint, payload);
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}
