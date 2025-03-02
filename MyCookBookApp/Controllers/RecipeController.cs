using Microsoft.AspNetCore.Mvc;
using MyCookBookApp.Services;
using MyCookBookApp.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace MyCookBookApp.Controllers
{
    [ApiController]
    [Route("Recipe")]
    public class RecipeController : Controller
    {
        private readonly RecipeService _recipeService;

        public RecipeController(RecipeService recipeService)
        {
            _recipeService = recipeService;
        }

        // ✅ Show the Recipe Index Page
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Index()
        {
            var recipes = await _recipeService.GetRecipesAsync();
            return View(recipes);
        }

        // ✅ Fetch All Recipes (GET /Recipe/GetAll)
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllRecipes()
        {
            var recipes = await _recipeService.GetRecipesAsync();
            return Json(recipes);
        }

        // ✅ Fetch Recipe by ID (GET /Recipe/{id})
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRecipeById(string id)
        {
            var recipe = await _recipeService.GetRecipeByIdAsync(id);
            if (recipe == null)
            {
                ViewData["ErrorMessage"] = "Recipe not found.";
                return View("Index", new List<Recipe>());
            }
            return Json(recipe);
        }

        // ✅ Search for Recipes (POST /Recipe/Search)
        [HttpPost("Search")]
        public async Task<IActionResult> Search([FromBody] RecipeSearchRequest searchRequest)
        {
            if (searchRequest == null || string.IsNullOrWhiteSpace(searchRequest.Keyword))
            {
                ViewData["ErrorMessage"] = "Please enter a keyword to find recipes.";
                return View("Index", new List<Recipe>());
            }

            var recipes = await _recipeService.SearchRecipesAsync(searchRequest);

            if (recipes == null || recipes.Count == 0)
            {
                ViewData["ErrorMessage"] = "No matching recipes found.";
                return View("Index", new List<Recipe>());
            }
            return View("Index", recipes);
        }

        // ✅ Add a Recipe (POST /Recipe/Add)
        [HttpPost("Add")]
        public async Task<IActionResult> AddRecipe([FromBody] Recipe recipe)
        {
            Console.WriteLine("Received Recipe: " + JsonConvert.SerializeObject(recipe));

            if (recipe == null || string.IsNullOrWhiteSpace(recipe.Name))
            {
                ViewData["ErrorMessage"] = "Invalid recipe data. Name is required.";
                return View("Index", new List<Recipe>());
            }

            bool added = await _recipeService.AddRecipeAsync(recipe);

            if (!added)
            {
                ViewData["ErrorMessage"] = "Failed to add recipe.";
                return View("Index", new List<Recipe>());
            }

            return Json(new { success = true, message = "Recipe added successfully!" });
        }
    }
}
