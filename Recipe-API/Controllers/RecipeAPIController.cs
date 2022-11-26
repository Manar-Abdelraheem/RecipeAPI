using Microsoft.AspNetCore.Mvc;
using Recipe_API.Data;
using Recipe_API.Models;
using Recipe_API.Models.DTO;

namespace Recipe_API.Controllers
{
    [Route("api/Recipes")]
    [ApiController]
    public class RecipeAPIController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult< IEnumerable<RecipeDTO>> GetRecipes() 
        {
            return Ok( RecipeStore.recipeList);
        }
        [HttpGet("{id:int}", Name ="GetRecipe")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult< RecipeDTO> GetRecipe(int id)
        {
            if (id == 0) 
            {
                return BadRequest();
            }

            var recipe = RecipeStore.recipeList.FirstOrDefault(u => u.Id == id);
            if(recipe == null) 
            {
                return NotFound();
            }
            return Ok(recipe);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<RecipeDTO> CreateRecipe([FromBody]RecipeDTO recipeDTO)
        {
            if (recipeDTO == null) 
            {
                return BadRequest(recipeDTO);
            }
            if (recipeDTO.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            recipeDTO.Id = RecipeStore.recipeList.OrderByDescending(u => u.Id).FirstOrDefault().Id + 1;
            RecipeStore.recipeList.Add(recipeDTO);
            return CreatedAtRoute("GetRecipe",new { id= recipeDTO.Id}, recipeDTO);
        }
    }
}
