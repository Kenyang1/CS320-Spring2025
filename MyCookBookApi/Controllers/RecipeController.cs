using Microsoft.AspNetCore.Mvc;
using MyCookBookApi.Models;
using MyCookBookApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyCookBookApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecipeController : ControllerBase
    {
        // Static list retains your previous data for testing/search functionality.
        private static readonly List<Recipe> Recipes = new List<Recipe>
        {
            new Recipe 
            { 
                RecipeId = Guid.NewGuid().ToString(), 
                Name = "Pasta", 
                TagLine = "Tasty Pasta",
                Summary = "A classic Italian pasta dish.",
                Ingredients = new List<string> { "Pasta", "Tomato Sauce" }, 
                Instructions = new List<string> { "Boil pasta." }
                
            },
            new Recipe 
            { 
                RecipeId = Guid.NewGuid().ToString(), 
                Name = "Salad", 
                TagLine = "Freshly Made Salad",
                Summary = "A healthy salad with mixed greens.",
                Ingredients = new List<string> { "Lettuce", "Tomatoes" }, 
                Instructions = new List<string> { "Mix all ingredients." }
            }
        };

        private readonly IRecipeService _recipeService;

        public RecipeController(IRecipeService recipeService)
        {
            _recipeService = recipeService;
        }

        // GET: api/recipe
        [HttpGet]
        public IActionResult GetRecipes()
        {
            return Ok(_recipeService.GetAllRecipes());
        }

        [HttpPost("search")]
        public IActionResult Search([FromBody] RecipeSearchRequest request)
        {
            // Validate the search query
            if (string.IsNullOrWhiteSpace(request.Keyword))
            {
                return BadRequest("Invalid search query. Please enter a valid recipe name.");
            }

            if (request.Keyword.Any(ch => !char.IsLetterOrDigit(ch) && !char.IsWhiteSpace(ch)))
            {
                return BadRequest("Invalid search query. Special characters are not allowed.");
            }
            
            // Ensure Categories is never null
            request.Categories ??= new List<CategoryType>();

            // Delegate the search functionality to the service layer.
            var recipes = _recipeService.SearchRecipes(request);
            return Ok(recipes);
        }


        // POST: api/recipe
        [HttpPost]
        public IActionResult CreateRecipe([FromBody] Recipe recipe)
        {
            if (recipe == null || string.IsNullOrWhiteSpace(recipe.Name))
            {
                return BadRequest("Invalid recipe data.");
            }

            if (string.IsNullOrWhiteSpace(recipe.RecipeId))
            {
                recipe.RecipeId = Guid.NewGuid().ToString();
            }

            recipe.RecipeId ??= Guid.NewGuid().ToString();


            if (string.IsNullOrWhiteSpace(recipe.TagLine) || string.IsNullOrWhiteSpace(recipe.Summary))
            {
                return BadRequest("TagLine and Summary are required.");
            }

            // Add the recipe to the repository using the service layer
            _recipeService.AddRecipe(recipe);
            return CreatedAtAction(nameof(GetRecipeById), new { id = recipe.RecipeId }, recipe);
        }

        [HttpGet("{id}")] 
        public IActionResult GetRecipeById(string id) {
            var recipe = _recipeService.GetRecipeById(id);
            if (recipe == null) 
            {
                return NotFound();
            }
            return Ok(recipe);
        }
    }
}
