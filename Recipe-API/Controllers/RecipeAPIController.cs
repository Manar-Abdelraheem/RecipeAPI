using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Recipe_API.Data;
using Recipe_API.Models;
using Recipe_API.Models.DTO;
using System.Xml.Linq;
using System.IO;

namespace Recipe_API.Controllers
{
    [Route("api/Recipes")]
    [ApiController]
    public class RecipeAPIController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<RecipeDTO>>> GetRecipesAsync() 
        {
            return Ok(await ReadAndWrite.ReadRecipeFileAsync());
        }

        [HttpGet("{id:int}", Name ="GetRecipe")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task< ActionResult<RecipeDTO>> GetRecipeAsync(int id)
        {
            if (id == 0) 
            {
                return BadRequest();
            }
            var recipelist = await ReadAndWrite.ReadRecipeFileAsync();
            var recipe = recipelist.FirstOrDefault(x => x.Id == id);
            if (recipe == null) 
            {
                return NotFound();
            }
            return Ok(recipe);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<RecipeDTO>> CreateRecipe([FromBody] RecipeDTO recipeDTO)
        {
            List<RecipeDTO>? recipeList = new();
            recipeList = await ReadAndWrite.ReadRecipeFileAsync();
            if (recipeDTO == null)
            {
                return BadRequest(recipeDTO);
            }
            if (recipeList!.FirstOrDefault(x => x!.Title!.ToLower() == recipeDTO!.Title!.ToLower()) != null)
            {
                ModelState.AddModelError("CustomeError", "Recipe already exists!");
                return BadRequest(ModelState);
            }
            if (recipeDTO.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            recipeDTO.Id = (recipeList?.OrderByDescending(x => x?.Id ?? 0)?.FirstOrDefault() ?.Id ?? 0) + 1;
            recipeList?.Add(recipeDTO);
            await ReadAndWrite.WriteRecipeFileAsync(recipeList);
            return CreatedAtRoute("GetRecipe", new { id = recipeDTO.Id }, recipeDTO);
        }

        [HttpDelete("{id=int}", Name = "DeleteRecipe")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteRecipe(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var recipelist = await ReadAndWrite.ReadRecipeFileAsync();
            var recipe = recipelist.FirstOrDefault(x => x.Id == id);
            if (recipe == null)
            {
                return NotFound();
            }
            recipelist.Remove(recipe);
            await ReadAndWrite.WriteRecipeFileAsync(recipelist);
            return NoContent();
        }

        [HttpPut("{id:int}", Name ="UpdateRecipe")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateRecipe(int id,[FromBody]RecipeDTO recipeDTO) 
        {
            if (recipeDTO == null || id != recipeDTO.Id)
            {
                return BadRequest();
            }
            var recipelist = await ReadAndWrite.ReadRecipeFileAsync();
            var recipe = recipelist.FirstOrDefault(x => x.Id == id);
            if (recipe != null)
            {
                recipe.Title = recipeDTO.Title;
                recipe.Ingredients = recipeDTO.Ingredients;
                recipe.Instructions = recipeDTO.Instructions;
                recipe.Categories = recipeDTO.Categories;
            }
            await ReadAndWrite.WriteRecipeFileAsync(recipelist);
            return NoContent();
        }
    }  
}
