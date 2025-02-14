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

            if (string.IsNullOrWhiteSpace(request.Query)) {
                return BadRequest("Invalid search query. Please enter a valid recipe name.");
            }

            if (request.Query.Any(ch => !char.IsLetterOrDigit(ch) && !char.IsWhiteSpace(ch)))
            {
                return BadRequest("Invalid search query. Special characters are not allowed.");
            }            
            
            var results = Recipes
                .Where(r => r.Name.Contains(request.Query, System.StringComparison.OrdinalIgnoreCase))
                .ToList();
                

            return Ok(results);
        }
    }
}
