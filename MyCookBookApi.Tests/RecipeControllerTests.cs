using Xunit; // XUnit is used for testing
using MyCookBookApi.Controllers; // Reference to the RecipeController class
using MyCookBookApi.Models; // Reference to the models (like Recipe and RecipeSearchRequest)
using Microsoft.AspNetCore.Mvc; // Necessary for working with ActionResults (OkObjectResult, BadRequestObjectResult)
using System; // For StringComparison
using System.Collections.Generic; // For List<T>

namespace MyCookBookApi.Tests
{
    public class RecipeControllerTests
    {
        [Fact]
        public void Search_ShouldReturnRecipes_WhenQueryIsValid()
        {
            // Step 1: Set up the test scenario
            var controller = new RecipeController();

            // Step 2: Call the Search method
            var result = controller.Search(new RecipeSearchRequest { Query = "Pasta" });

            // Step 3: Assert: Check if the result is what we expected
            var okResult = Assert.IsType<OkObjectResult>(result);
            var recipes = Assert.IsType<List<Recipe>>(okResult.Value);
            Assert.NotEmpty(recipes);

            foreach (var recipe in recipes)
            {
                Assert.Contains("Pasta", recipe.Name, StringComparison.OrdinalIgnoreCase);
            }
        }
    }
}
