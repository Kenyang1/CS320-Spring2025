using Microsoft.AspNetCore.Mvc;
using MyCookBookApp.Services;
using MyCookBookApp.Models;
using System.Threading.Tasks;

namespace MyCookBookApp.Controllers
{
    public class RecipeController : Controller
    {
        private readonly RecipeService _recipeService;
        public RecipeController(RecipeService recipeService)
        {
            _recipeService = recipeService;
        }

        public async Task<IActionResult> Index()
        {
            var recipes = await _recipeService.GetRecipesAsync();
            return View(recipes);
        }

        public async Task<IActionResult> Search(string query) 
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                ViewData["ErrorMessage"] = "Please enter a keyword to find recipes";
                return View("Index", new List<Recipe>());
            }
            var recipes = await _recipeService.SearchRecipesAsync(query);

            if (recipes == null) {
                ViewData["ErrorMessage"] = "Error has occured while searching.";
                return View("Index", new List<Recipe>());
            }
            return View("Index", recipes);
        }
        

    }
}
