using Recipe_API.Models.DTO;

namespace Recipe_API.Data
{
    public static class RecipeStore
    {
        public static List<RecipeDTO> RecipeList = new()
        {
                new RecipeDTO{ Id = 1, Title ="molokhia", Ingredients = "molokhia, meat soup", Instructions = "boil the soup", Categories = ""},
                new RecipeDTO{ Id = 2, Title = "bamia", Ingredients = "bamia, meat soup", Instructions = "boil the soup", Categories = ""}
        };
    }
}
