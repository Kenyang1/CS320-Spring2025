using Xunit;
using MyCookBookApi.Controllers;
using MyCookBookApi.Models;
using Microsoft.AspNetCore.Mvc;
using Moq; // Moq for mocking dependencies
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyCookBookApi.Tests
{
    public class RecipeControllerTests
        {
            [Fact]
        public void Search_ShouldReturnInvalidMessage_WhenQueryHasSpecialCharacters()
        {
            // Arrange: Create a test instance of the controller with sample recipes
            var controller = new RecipeController();

            var request = new RecipeSearchRequest { Query = "p@sta!" };

            // Act: Call the Search method
            var result = controller.Search(request);

            // Assert: Ensure the response is a BadRequest with the expected error message
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var errorMessage = badRequestResult.Value as string;

            Assert.Equal("Invalid search query. Special characters are not allowed.", errorMessage);
        }
    }
}