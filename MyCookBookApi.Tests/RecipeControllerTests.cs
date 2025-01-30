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
        public void Search_ShouldReturnEmptyList_WhenQueryDoesNotMatchAnyRecipe()
        {
            // Step 1: Set up the test scenario
            var controller = new RecipeController();

            // Step 2: Call the Search method with a query that doesn't exist
            var result = controller.Search(new RecipeSearchRequest { Query = "Pizza" });

            // Step 3: Assert: Check if the result is what we expected
            var okResult = Assert.IsType<OkObjectResult>(result);
            var recipes = Assert.IsType<List<Recipe>>(okResult.Value);
            Assert.Empty(recipes);
        }
        [Fact]
        public void Search_ShouldReturnBadRequest_WhenQueryIsOnlySpace(){
            // Step 1: Set up the test scenario
            var controller = new RecipeController();
            //Step 2: Call the search method with a query that is Case sensitive
            var result = controller.Search(new RecipeSearchRequest {Query = " "});

            //Step 3: Assert: Check if the result is what we expected
            // Check if it returns a space
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var errorMessages = Assert.IsType<string>(badRequestResult.Value);
            Assert.Contains("Query cannot be empty.", errorMessages);
        }
    
    }
}
