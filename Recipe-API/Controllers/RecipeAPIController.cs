using Microsoft.AspNetCore.Mvc;
using Recipe_API.Models;

namespace Recipe_API.Controllers
{
    [Route("api/RecipeAPI")]
    [ApiController]
    public class RecipeAPIController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Recipe> GetRecipes() 
        {
            return new List<Recipe>
            {
                new Recipe{ Id=1,Title="molokhia"},
                new Recipe{ Id=2,Title="bamia"}
            };
        }

    }
}
