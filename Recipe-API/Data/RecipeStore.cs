﻿using Recipe_API.Models.DTO;

namespace Recipe_API.Data
{
    public static class RecipeStore
    {
        public static List<RecipeDTO> recipeList = new List<RecipeDTO>
        {
                new RecipeDTO{ Id=1,Title="molokhia"},
                new RecipeDTO{ Id=2,Title="bamia"}
        };
    }
}
