using Microsoft.AspNetCore.Mvc;
using MyCookBookApi.Controllers;
using MyCookBookApi.Models;
using MyCookBookApi.Services;
using Xunit;
using Moq;
using System.Collections.Generic;

public class RecipeControllerTests
{
    private readonly RecipeController _controller;
    private readonly Mock<IRecipeService> _mockService;

    public RecipeControllerTests()
    {
        // We use Moq to create a fake RecipeService.
        _mockService = new Mock<IRecipeService>();
        _controller = new RecipeController(_mockService.Object);
    }

    [Fact]
    public void GetAllRecipes_ReturnsOkResult()
    {
        // Arrange: Create a fake list with one recipe.
        var fakeRecipes = new List<Recipe> { new Recipe { RecipeId = "1", Name = "Test Recipe" } };
        _mockService.Setup(s => s.GetAllRecipes()).Returns(fakeRecipes);

        // Act: Call the controller's GetAllRecipes method.
        var result = _controller.GetAllRecipes();

        // Assert: Check that the result is OK and contains one recipe.
        var actionResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedRecipes = Assert.IsType<List<Recipe>>(actionResult.Value);
        Assert.Single(returnedRecipes);
    }

    [Fact]
    public void GetRecipeById_WhenRecipeExists_ReturnsOk()
    {
        // Arrange: Create a fake recipe with ID "123".
        var fakeRecipe = new Recipe { RecipeId = "123", Name = "Pasta" };
        _mockService.Setup(s => s.GetRecipeById("123")).Returns(fakeRecipe);

        // Act: Call GetRecipeById with "123".
        var result = _controller.GetRecipeById("123");

        // Assert: Ensure the controller returns an OK result with the recipe.
        var actionResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedRecipe = Assert.IsType<Recipe>(actionResult.Value);
        Assert.Equal("123", returnedRecipe.RecipeId);
    }

    [Fact]
    public void GetRecipeById_WhenRecipeDoesNotExist_ReturnsNotFound()
    {
        // Arrange: Setup the service to return null for a non-existent ID.
        _mockService.Setup(s => s.GetRecipeById("999")).Returns((Recipe)null);

        // Act: Call GetRecipeById with "999".
        var result = _controller.GetRecipeById("999");

        // Assert: The result should be a 404 Not Found.
        Assert.IsType<NotFoundResult>(result.Result);
    }
}
