﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RecipeUI.Data;
using RecipeUI.Models;
using System.Text.Json;

namespace RecipeUI.Pages
{
    public class IndexModel : PageModel
    {
        public static HttpClient client = new HttpClient();
        public static Uri endPoint = new Uri("https://localhost:7208/api/Recipes");
        private readonly ILogger<IndexModel> _logger;
        public IndexModel(ILogger<IndexModel> logger, RecipeDbContext db)
        {
            _logger = logger;
        }
        public IEnumerable<Recipe> Recipes { get; set; }
        public async Task OnGet()
        {
            var result = await client.GetAsync(endPoint);
            var options = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            using var json = await result.Content.ReadAsStreamAsync();
            Recipes = await JsonSerializer.DeserializeAsync<List<Recipe>>(json, options);
        }
    }
}