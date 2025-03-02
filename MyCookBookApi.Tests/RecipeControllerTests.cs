using Xunit;
using MyCookBookApi.Controllers;
using MyCookBookApi.Models;
using MyCookBookApi.Services;
using Microsoft.AspNetCore.Mvc;
using Moq; // For mocking dependencies
using System.Collections.Generic;

public class RecipeControllerTests
{
    private readonly RecipeController _controller;
    private readonly Mock<IRecipeService> _mockService;

    public RecipeControllerTests()
    {
        _mockService = new Mock<IRecipeService>();
        _controller = new RecipeController(_mockService.Object);
    }

    // ✅ Test Case 1: Calling GET /api/recipe returns an OK response with recipe list
    [Fact]
    public void GetAllRecipes_ReturnsOkResult()
    {
        // Arrange
        var fakeRecipes = new List<Recipe>
        {
            new Recipe { RecipeId = "1", Name = "Pasta" },
            new Recipe { RecipeId = "2", Name = "Salad" }
        };
        _mockService.Setup(s => s.GetAllRecipes()).Returns(fakeRecipes);

        // Act
        var result = _controller.GetRecipes();

        // Assert
        var actionResult = Assert.IsType<OkObjectResult>(result);
        var returnedRecipes = Assert.IsType<List<Recipe>>(actionResult.Value);
        Assert.Equal(2, returnedRecipes.Count);
    }

    // ✅ Test Case 2: Searching for a recipe using POST /api/recipe/search returns relevant data
    [Fact]
    public void SearchRecipes_WithValidKeyword_ReturnsRelevantData()
    {
        // Arrange
        var searchRequest = new RecipeSearchRequest { Keyword = "Pasta", Categories = new List<CategoryType>() };
        var fakeRecipes = new List<Recipe> { new Recipe { RecipeId = "1", Name = "Pasta" } };
        _mockService.Setup(s => s.SearchRecipes(searchRequest)).Returns(fakeRecipes);

        // Act
        var result = _controller.Search(searchRequest);

        // Assert
        var actionResult = Assert.IsType<OkObjectResult>(result);
        var returnedRecipes = Assert.IsType<List<Recipe>>(actionResult.Value);
        Assert.Single(returnedRecipes);
        Assert.Equal("Pasta", returnedRecipes[0].Name);
    }

    // ✅ Test Case 3: Fetching a recipe with a non-existent ID returns 404 Not Found
    [Fact]
    public void GetRecipeById_WhenRecipeDoesNotExist_ReturnsNotFound()
    {
        // Arrange
        _mockService.Setup(s => s.GetRecipeById("999")).Returns((Recipe)null);

        // Act
        var result = _controller.GetRecipeById("999");

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    // ✅ Test Case 4: Adding a new recipe successfully stores it in the repository
    [Fact]
    public void CreateRecipe_WithValidData_ReturnsCreatedAtAction()
    {
        // Arrange
        var newRecipe = new Recipe
        {
            RecipeId = "new_id",
            Name = "Chocolate Cake",
            TagLine = "Deliciously Rich",
            Summary = "A decadent and moist chocolate cake.",
            Ingredients = new List<string> { "Flour", "Sugar", "Cocoa Powder", "Eggs", "Milk" },
            Instructions = new List<string> { "Mix dry ingredients.", "Add wet ingredients.", "Bake at 350°F for 30 minutes." },
            Categories = new List<CategoryType>(),
            Media = new List<RecipeMedia>()
        };

        _mockService.Setup(s => s.AddRecipe(newRecipe));

        // Act
        var result = _controller.CreateRecipe(newRecipe);

        // Assert
        var actionResult = Assert.IsType<CreatedAtActionResult>(result);
        var returnedRecipe = Assert.IsType<Recipe>(actionResult.Value);
        Assert.Equal("Chocolate Cake", returnedRecipe.Name);
    }
}
