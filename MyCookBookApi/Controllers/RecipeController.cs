using Microsoft.AspNetCore.Mvc;
using MyCookBookApi.Models;
using System.Linq;

namespace MyCookBookApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecipeController : ControllerBase
    {
        private static readonly List<Recipe> Recipes = new List<Recipe>
        {
            new Recipe { Id = 1, Name = "Pasta", Ingredients = new List<string> { "Pasta", "Tomato Sauce" }, Steps = "Boil pasta." },
            new Recipe { Id = 2, Name = "Salad", Ingredients = new List<string> { "Lettuce", "Tomatoes" }, Steps = "Mix all ingredients." }
        };

        // GET: api/recipe
        [HttpGet]
        public IActionResult GetRecipes()
        {
            return Ok(Recipes);
        }

        // POST: api/recipe/search
        [HttpPost("search")]
        public IActionResult Search([FromBody] RecipeSearchRequest request)
        {
            //Checks if theres a space in query
            if (request == null || string.IsNullOrWhiteSpace(request.Query))
            {
                return BadRequest(new { message = "There are no recipes with that name. "});
                
            }

            var results = Recipes
                .Where(r => r.Name.Contains(request.Query, System.StringComparison.OrdinalIgnoreCase))
                .ToList();
                
            if (!results.Any()) {
                return BadRequest("No matching recipes found.");
            }

            return Ok(results);
        }
    }
}
