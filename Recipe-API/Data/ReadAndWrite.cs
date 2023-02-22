using Recipe_API.Models.DTO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Recipe_API.Data
{
    public class ReadAndWrite
    {
        public  static async Task<List<RecipeDTO>> ReadRecipeFileAsync()
        {
            var deserializationString = await File.ReadAllTextAsync(@"Recipe.json");
            if (string.IsNullOrWhiteSpace(deserializationString))
                { return new List<RecipeDTO>(); }
            var jsonObject = JsonSerializer.Deserialize<List<RecipeDTO>>(deserializationString);           
            if (jsonObject == null )
            {
                return new List<RecipeDTO> { new RecipeDTO() };
            }
            else
                return jsonObject;
        }

        public static async Task WriteRecipeFileAsync(List<RecipeDTO>? recipeList)
        {
            var jsonString = JsonSerializer.Serialize(recipeList);
            await File.WriteAllTextAsync(@"Recipe.json", jsonString);
        }
    }
}
