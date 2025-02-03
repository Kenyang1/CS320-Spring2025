using Xunit;
using MyCookBookApi.Controllers;
using MyCookBookApi.Models;
using Microsoft.AspNetCore.Mvc;
using MyCookBookApp.Services;
using Moq; // Moq for mocking dependencies
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyCookBookApi.Tests
{
    public class RecipeControllerTests
    {
        // Test when query doesn't match any recipe
        [Fact]
        public async Task Search_ShouldReturnEmptyList_WhenQueryDoesNotMatchAnyRecipe()
        {
            // Arrange: Create a mock RecipeService
            var mockRecipeService = new Mock<RecipeService>(MockBehavior.Strict, new System.Net.Http.HttpClient());
            mockRecipeService.Setup(service => service.SearchRecipesAsync("Pizza"))
                             .ReturnsAsync(new List<Recipe>()); // No matching recipe

            // Create a controller and pass the mocked RecipeService
            var controller = new RecipeController(mockRecipeService.Object);

            // Act: Call the Search method with a query that doesn't match any recipe
            var result = await controller.Search("Pizza");

            // Assert: Check if the result is what we expected
            var okResult = Assert.IsType<OkObjectResult>(result);
            var recipes = Assert.IsType<List<Recipe>>(okResult.Value);
            Assert.Empty(recipes); // Ensure the list is empty
        }

        // Test when the query is empty (or just whitespace)
        [Fact]
        public async Task Search_ShouldReturnEmptyList_WhenQueryIsEmpty()
        {
            // Arrange: Create a mock RecipeService
            var mockRecipeService = new Mock<RecipeService>(MockBehavior.Strict, new System.Net.Http.HttpClient());
            mockRecipeService.Setup(service => service.SearchRecipesAsync(It.IsAny<string>()))
                             .ReturnsAsync(new List<Recipe>()); // Return an empty list for any query

            var controller = new RecipeController(mockRecipeService.Object);

            // Act: Call the Search method with an empty string
            var result = await controller.Search("");

            // Assert: Check if the result is an empty list
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<List<Recipe>>(viewResult.Model);
            Assert.Empty(model); // Ensure the model is empty
        }

        // You can add more tests as needed for different scenarios (valid query, invalid query, etc.)
    }
}
