using Microsoft.AspNetCore.Mvc;
using RecipeUI.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel;
using static System.Net.Mime.MediaTypeNames;
using System.Text.Json;
using System.Text;
using System.Net.Http;

namespace RecipeUI.Pages
{
        [BindProperties]
    public class CreateModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CreateModel(IHttpClientFactory httpClientFactory) => _httpClientFactory = httpClientFactory;
        public void OnGet()
        {

        }
        public Recipe Recipe { get; set; } = default!;
        public async Task<IActionResult> OnPost(Recipe recipe) 
        {
            if (recipe == null) 
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                var httpClient = _httpClientFactory.CreateClient();
                var json = new StringContent(JsonSerializer.Serialize(recipe),Encoding.UTF8,Application.Json);
                using var httpResponseMessage = await httpClient.PostAsync("https://localhost:7208/api/Recipes", json);
                httpResponseMessage.EnsureSuccessStatusCode();
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}
